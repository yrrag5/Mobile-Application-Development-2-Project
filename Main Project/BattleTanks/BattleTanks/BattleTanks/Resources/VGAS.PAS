{
 .vga picture unit
 fast assembler puts
 use 256Paint or save with savevga,
 to make vga files
 Landon W. Rabern, 1998
}
unit vgas;

interface

uses mcga;
type
    Tvga=record
               xs,ys:word;
               data:pointer;
    end;
    Tpal  = array[0..255,1..3] of byte;

procedure killvga(var p:Tvga);
procedure loadvga(var vga:Tvga;fn:string);
procedure loadpal(var pvar:Tpal;fname:string);
procedure setallpal(var pvar:Tpal);
procedure putvga(x,y:integer;vga:Tvga);
procedure putcvga(x,y:integer;vga:Tvga);
procedure getvga(x1,y1,x2,y2:integer;var bitmap:Tvga);
procedure savevga(vga:Tvga;fn:string);

implementation

procedure killvga(var p:Tvga);
begin
     if p.data=nil then begin
        exit;
     end;
     freemem(p.data,p.xs*p.ys);
end;

procedure loadvga(var vga:Tvga;fn:string);
var
   f:file;
   header:array[1..4] of byte;
begin
     vga.data:=nil;
     assign(f,fn);
     reset(f,1);
     blockread(f,header[1],4);
     vga.xs:=1+memw[seg(header[1]):ofs(header[1])];
     vga.ys:=1+memw[seg(header[1]):ofs(header[1])+2];
     getmem(vga.data,vga.xs*vga.ys);
     blockread(f,vga.data^,vga.xs*vga.ys);
     close(f);
end;

procedure putvga(x,y:integer;vga:Tvga);
label
     row;
var
   xstrt,ystrt,off,xlen,ylen,skip:word;
   data:pointer;
begin
     if (x>319) or (x<=-vga.xs) or (y<=-vga.ys) or (y>199) then exit;
     xstrt:=0;
     ystrt:=0;
     if x<0 then xstrt:=-x;
     if y<0 then ystrt:=-y;
     xlen:=vga.xs-xstrt;
     ylen:=vga.ys-ystrt;
     if (x+vga.xs)>320 then xlen:=320-x-xstrt;
     if (y+vga.ys)>200 then ylen:=200-y-ystrt;
     if x>0 then off:=x
     else off:=0;
     if y>0 then off:=off+scr_ofs[y];
     data:=ptr(seg(vga.data^),ofs(vga.data^)+ystrt*vga.xs);
     if x>0 then skip:=vga.xs-(xlen-xstrt)
     else skip:=0;
     asm
        push ds
        les di,where
        add di,off
        lds si,data
        mov ax,ylen
        mov dx,di
        mov bx,xlen
       row:
        add si,xstrt
        mov cx,bx
        rep movsb
        add si,skip
        add dx,320
        mov di,dx
        dec ax
        jnz row
        pop ds
     end;
end;

procedure putcvga(x,y:integer;vga:Tvga);
label
     row,nd,col,done,newl;
var
   data:pointer;
   xstrt,ystrt,off,xlen,ylen,skip:word;
begin
     if (x>319) or (x<=-vga.xs) or (y<=-vga.ys) or (y>199) then exit;
     xstrt:=0;
     ystrt:=0;
     if x<0 then xstrt:=-x;
     if y<0 then ystrt:=-y;
     xlen:=vga.xs-xstrt;
     ylen:=vga.ys-ystrt;
     if (x+vga.xs)>320 then xlen:=320-x-xstrt;
     if (y+vga.ys)>200 then ylen:=200-y-ystrt;
     if x>0 then off:=x
     else off:=0;
     if y>0 then off:=off+scr_ofs[y];
     data:=ptr(seg(vga.data^),ofs(vga.data^)+ystrt*vga.xs);
     if x>0 then skip:=vga.xs-(xlen-xstrt)
     else skip:=0;
     asm
        push ds
        les  di,where
        add di,off
        lds si,data
        mov bx,ylen
        mov dx,di
       row:
        mov cx,xlen
        add si,xstrt
       col:
        lodsb
        or al,al
        jz nd
        stosb
        loop col
       newl:
        add si,skip
        add dx,320
        mov di,dx
        dec bx
        jnz row
        jmp done
       nd:
        inc di
        loop col
        jmp newl
       done:
        pop ds
     end;
end;

procedure loadpal(var pvar:tpal;fname:string);
var f:file of tpal;
begin
     assign(f,fname);
     {$I-}
     reset(f);
     {$I+}
     if ioresult<>0 then exit;
     read(f,pvar);
     close(f);
end;

procedure setallpal(var pvar:tpal);
var palc:byte;
begin
for palc := 0 to 255 do
    begin
    port[$3c8] := palc;
    port[$3c9] := pvar[palc,1];
    port[$3c9] := pvar[palc,2];
    port[$3c9] := pvar[palc,3];
    end;
end;

procedure getvga(x1,y1,x2,y2:integer;var bitmap:Tvga);
var
   i,line,off:word;
begin
     line:=x1+scr_ofs[y1];
     off:=0;
     bitmap.xs:=abs(x1-x2);
     bitmap.ys:=abs(y1-y2);
     getmem(bitmap.data,bitmap.xs*bitmap.ys);
     for i:=1 to bitmap.ys do begin
         move(mem[seg(where^):ofs(where^)+line],mem[seg(bitmap.data^):off],bitmap.xs);
         inc(line,320);
         inc(off,bitmap.xs);
     end;
     dec(bitmap.xs);
     dec(bitmap.ys);
end;

procedure savevga(vga:Tvga;fn:string);
var
   f:file;
   header:array[1..2] of word;
begin
     assign(f,fn);
     rewrite(f,1);
     header[1]:=vga.xs-1;
     header[2]:=vga.ys-1;
     blockwrite(f,header[1],4);
     blockwrite(f,vga.data^,vga.xs*vga.ys);
end;

end.
