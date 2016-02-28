2
2
4
2048
2
4
AddrModes:
imm 3 15
regind 1 4
memdir 3 9
memind 3 10
InstructionSet:
ld 16
AM: regind imm memdir memind 
st 32
AM: regind memdir memind 
jmp 48
AM: memdir 
add 64
AM: imm regind memdir memind 
sub 80
AM: imm regind memdir memind 
mul 96
AM: imm regind memdir memind 
div 112
AM: imm regind memdir memind 
jgr 128
AM: memdir 
jeq 144
AM: memdir 
jls 160
AM: memdir 
jge 176
AM: memdir 
jle 192
AM: memdir 
push 208
AM: 
pop 224
AM: 
halt 240
AM: 
END