# EasyBudget Api

O EasyBudget Api é uma api em Asp.net Core para realizar a gestão financeira pessoal, dando a possibilidade de cadastar, ler, atualizar e deletar seus movimentos financeiros.

# Como executar o projeto e criar o banco de dados?

O projeto utiliza Entity Framework Core para criar os arquivos de Migration do banco de dados SQL. Para saber como executar o migration segue a [documentação oficial da Microsoft](https://learn.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

# Funcionalidades

# Lançamentos

## Inserir

O cliente poderá inserir lançamentos de 2 tipos:

* Receitas
* Despesas

E os lançamentos podem ser agrupados por categorias pré definidas ou adicionadas pelo usuário. As categorias seriam como Plano de Contas. Categorias pré definidas para despesas:

* Transporte
* Comida
* Mercado
* Saúde
* Trabalho
* Educação
* Casa
* Investimento
* Outros

Categorias pré definidas para receitas:

* Salário
* Investimento
* Outras receitas

Os lançamentos deverão ter obrigatoriamente as seguintes informações:

* Valor: valor da transação

* Titulo: um breve título sobre o que a transação representa (Ex: Creme de pentear)

* Data: a data em que a transação foi realizada (num primeiro momento, será aceito apenas datas menores ou igual à data atual)

* Tipo: se o lançamento é uma receita ou uma despesa

* Categoria: em qual categoria esse lançamento se encaixa (Ex: Transporte, Comida, Mercado, Trabalho, Viagem, Saúde, Educação)

E poderá opcionalmente acrescentar:

* Descrição: uma breve descrição sobre a transação (Ex: Creme de pentear da Salon Line que comprei no mercado perto de casa depois do trabalho)

## Atualizar

Todos os campos de todos os lançamentos poderão ser atualizados.

## Listar

Os lançamentos poderão ser listados com a possbibilidade de utilizar filtros nos campos dos lançamentos e ordenar a listagem a partir de algum campo.

## Deletar

Todos os lançamentos poderão ser deletados.

# Categoria

## Inserir

O usuário poderá inserir novas categorias além das pré definidas.

A categorias deverão ter obrigatoriamente os campos:

* Titulo: titulo da categoria

* Tipo: pode ser de receita ou de despesa

## Atualizar

Poderá ser atualizado apenas o título da categoria.

## Deletar

Qualquer categoria poderá ser deletada. Todos os lançamentos que tiverem vinculados à categoria que está sendo deletada, terá a sua categoria alterada Outros/Outras receitas, a depender do tipo do lançamento.

## Listar

As categorias poderão ser listadas.

# Saldo

O saldo será a soma de todas os lançamentos realizados, levando em conta as receitas e despesas.