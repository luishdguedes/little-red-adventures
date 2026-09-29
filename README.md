# Little Red Adventures

Projeto de jogo 2D desenvolvido no Unity para a Sprint 4. O jogo reúne cenas de menu, fases, inimigos, combate e progressão entre níveis.

## Requisitos

- Unity **2022.3.25f1** (versão usada no projeto).

## Abrir o projeto

1. Clone este repositório.
2. No Unity Hub, selecione **Add project from disk** e escolha a pasta clonada.
3. Abra o projeto com Unity 2022.3.25f1 e aguarde a importação dos assets.
4. Para iniciar pelo menu, abra `Assets/TelaStart/TelaInicio.unity`. As cenas habilitadas para build estão configuradas em **File > Build Profiles** (ou **Build Settings**, dependendo da versão do Editor).

## Estrutura

- `Assets/` — cenas, scripts, sprites, animações e demais recursos do jogo.
- `Packages/` — dependências gerenciadas pelo Unity Package Manager.
- `ProjectSettings/` — configurações do projeto e do Editor.

Os diretórios gerados pelo Unity, como `Library/`, `Temp/` e `Logs/`, não são versionados; o Editor os recria ao abrir o projeto.
