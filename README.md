# Little Red Adventures

**Little Red Adventures** é um jogo 2D de ação e plataforma desenvolvido em equipe para a disciplina de **Trabalho Interdisciplinar 4 (TI4)**. Inspirado por jogos de plataforma e ação, o projeto coloca Chapeuzinho em uma aventura contra inimigos, com fases, combate, exploração e uma personagem aliada.

O trabalho integra programação de jogos, animação, inteligência artificial e algoritmos de grafos em um projeto feito no Unity. A apresentação da Sprint 4 descreve três fases, incluindo uma fase tutorial e uma fase de chefe.

## Sobre o jogo

A pessoa jogadora controla Chapeuzinho em um mundo 2D, atravessa as fases, enfrenta inimigos e pode usar Selena para perseguir adversários. O projeto combina mecânicas de plataforma — movimentação lateral e pulo duplo — com combate e navegação de personagens.

### Controles

| Tecla | Ação |
| --- | --- |
| `W`, `A`, `S`, `D` | Movimentar-se, conforme indicado no tutorial do jogo |
| `Espaço` | Pular (o controle do personagem permite até dois pulos consecutivos) |
| `J` | Atacar |
| `E` | Ativar ou desativar o acompanhamento de um inimigo por Selena |
| `X` | Fechar a tela de tutorial |

### Sistemas e mecânicas

- **Movimentação e animações:** controle lateral, orientação do personagem, pulo e transições de animação.
- **Combate e vida:** ataques por colisão, dano e barras de vida para personagens e inimigos.
- **Inimigos:** patrulha entre pontos definidos e ataques quando o jogador entra no alcance.
- **Selena e navegação:** pode seguir o inimigo mais próximo; o caminho é recalculado sobre uma grade usando A*.
- **Grafos:** inclui uma estrutura de grafo com busca em largura (BFS), além da busca A* em grade.
- **Fases:** cenas de menu, tutorial, níveis e tela de fim de jogo, incluindo a fase de chefe.

## Tecnologias

- **Unity 2022.3.25f1** — versão do Editor usada no projeto.
- **C#** — scripts de movimentação, combate, inimigos, grafos e navegação.
- **Unity 2D** — cenas, física, animações, sprites e interface.

## Executar o projeto

1. Instale o Unity Hub e o **Unity Editor 2022.3.25f1**.
2. Clone este repositório e adicione a pasta clonada ao Unity Hub (**Add project from disk**).
3. Abra o projeto e aguarde o Unity importar os assets e pacotes.
4. Para começar pelo menu, abra `Assets/TelaStart/TelaInicio.unity`.
5. As cenas de jogo e a ordem de build estão definidas em `ProjectSettings/EditorBuildSettings.asset`.

O projeto usa cenas, assets e configurações armazenados em `Assets/`, `Packages/` e `ProjectSettings/`. Pastas locais geradas pelo Editor, como `Library/`, são reconstruídas pelo Unity e não fazem parte do repositório.

## Organização

- `Assets/TelaStart/` — menu inicial e tutorial.
- `Assets/Levels/` — fases, incluindo níveis e tela de fim de jogo.
- `Assets/Scripts/` — movimentação, combate, inimigos, grafos e pathfinding.
- `Packages/` — dependências gerenciadas pelo Unity Package Manager.
- `ProjectSettings/` — versão do Editor e configurações do projeto.

## Trabalho em equipe

Projeto realizado para o **Trabalho Interdisciplinar 4 (TI4)**. A apresentação da Sprint 4 registra as seguintes contribuições:

| Integrante | Contribuição apresentada |
| --- | --- |
| Luis Henrique Guedes | Coordenação de projetos e animações |
| Fabio Franco | Grafos |
| Pedro Rafael | Inteligência artificial |
| Thiago Teixeira | Grafos |
| Lucas Sena | Inteligência artificial |
| Samuel Quites | Inteligência artificial |

## Créditos

O conceito, o código e a apresentação são resultado de um trabalho acadêmico em equipe. O jogo apresenta referências a jogos de plataforma e ação, conforme descrito na apresentação da Sprint 4. Assets e ferramentas de terceiros permanecem sujeitos aos termos de seus respectivos autores e distribuidores.
