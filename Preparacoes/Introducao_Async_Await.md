# Introducao a async e await em C#

## Objetivo da aula
Hoje vamos aprender quando usar async e await de forma pratica.
A ideia nao e decorar sintaxe, e sim entender um problema real:
como lidar com esperas de API, arquivo ou banco sem travar a aplicacao.

## O que e sincronico e assincronico?
Sincronico:
- As tarefas acontecem em ordem, uma por vez.
- A proxima tarefa so comeca quando a anterior termina.
- Se uma operacao demora, o fluxo fica parado esperando.

Assincronico:
- O programa pode iniciar uma operacao demorada e nao ficar travado nela.
- Enquanto aguarda a resposta (API, arquivo, banco), a thread pode ser liberada.
- Quando a operacao termina, o fluxo continua do ponto do await.

Resumo simples:
- Sincronico = "espera bloqueando".
- Assincronico = "espera sem bloquear".

Importante:
Assincronico nao significa obrigatoriamente "mais rapido" em qualquer caso.
Ele melhora principalmente a responsividade e, quando combinado com paralelismo de tarefas independentes (Task.WhenAll), tambem pode reduzir o tempo total.

## Problema real
Imagine uma tela que precisa buscar tres dados:
- Perfil do usuario
- Pedidos
- Notificacoes

Se fizermos isso de forma sincronica, a aplicacao espera cada busca terminar para comecar a proxima.
Resultado: mais tempo total de espera e menos responsividade.

## O que vamos comparar no exemplo
No codigo, comparamos tres formas de fazer a mesma tarefa:

1. Versao sincronica
Cada chamada bloqueia a thread.
Tempo total: soma dos tempos de cada operacao.

2. Versao async com await sequencial
Nao bloqueia a thread enquanto aguarda.
Mas, por estar em sequencia, o tempo total ainda tende a ser a soma.

3. Versao async paralela com Task.WhenAll
Iniciamos as tarefas juntas e aguardamos tudo no final.
Tempo total tende a se aproximar da operacao mais lenta.

## Mensagem principal para levar da aula
- async e await melhoram responsividade em operacoes de I/O.
- O ganho de tempo aparece com mais clareza quando as tarefas sao independentes e executadas em paralelo.
- Nem todo problema precisa de async; use quando houver espera externa (rede, disco, banco, servicos).

## Leitura guiada do exemplo
Ao executar o programa, observe:
- A ordem dos logs em cada versao
- O tempo total mostrado no final de cada bloco
- A diferenca entre "nao bloquear" e "terminar mais rapido"

## Fechamento sugerido em sala
Pergunta para a turma:
"Se a tela precisa de dados independentes de tres APIs, qual abordagem faz mais sentido e por que?"

Resposta esperada:
Usar async com Task.WhenAll, porque melhora a experiencia do usuario e reduz o tempo total de montagem da tela.
