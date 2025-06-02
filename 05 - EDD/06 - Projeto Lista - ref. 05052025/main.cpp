#include <iostream>
#include <conio.h>

using namespace std;


// METHODS INIT


void isEmpty();
void insert();
void print();
void find();
void remove();
void menu();
void listar();
void incluir();

// GLOBAL VARIABLES

/// @brief Estrutura de Funcionario
struct S_Funcionario
{
private:
    int _prontuario; // unique
    string _nome;
    double _salario;
public:
    S_Funcionario(int prontuario, string nome, double salario)
    {
        setProntuario(prontuario);
        setNome (nome);
        setSalario(salario);
    };
    S_Funcionario();

    /// @brief Busca Dados do prontuário
    /// @param prontuario
    void setProntuario(int prontuario){ this->_prontuario = prontuario; }
    void setNome (string nome)
    {
        if (!nome.empty())
            this->_nome = nome;
    }
    void setSalario(double salario)
    {
        if (salario >= 0)
            this->_salario = salario;
    }
   
    int getProntuario() { return _prontuario; }
    string getNome() {return _nome; }
    double getSalario() { return _salario; }

};

// ---------------------------------------------------------
// | LISTA ENCADEADA
// ---------------------------------------------------------

struct Lista
{
    S_Funcionario *dado;
    Lista *ant;
};

Lista* init()
{
    return NULL;
}
bool isEmpty(Lista* lista)
{
    return (lista == NULL);
}
Lista* insert(Lista* lista, S_Funcionario *i)
{
    Lista* novo = new Lista();
    novo->dado = i;
    novo->ant = lista;
    return novo;
}
void print(Lista* lista)
{
    Lista* aux;
    aux = lista;
    cout << "==============================================" << endl;
    while (aux != NULL)
    {
        cout << "-----------" << endl;
        cout << "Prontuario: " << aux->dado->getProntuario() << endl;
        cout << "Nome: " << aux->dado->getNome() << endl;
        cout << "Salario: (R$) " << aux->dado->getSalario() << endl;
        cout << "-----------" << endl;
        aux = aux->ant;
    }
    cout << "==============================================" << endl;

   
   
}
Lista* find(Lista* lista, int i)
{
    Lista* aux;
    aux = lista;
    while (aux != NULL && aux->dado->getProntuario() != i)
    {
        aux = aux->ant;
    }
    return aux;
}
Lista* remove(Lista* lista, int i)
{
    Lista* aux;
    Lista* apoio = NULL;
   
    aux = lista;
    while (aux != NULL && aux->dado->getProntuario() != i)
    {
        apoio = aux;
        aux = aux->ant;
    }
    if (aux == NULL)
    {
        return lista;
    }
   
    if (apoio == NULL) // o valor encontrado estava no ultimo elemento
    {
        lista = aux->ant;
    }
    else
    {
        apoio->ant = aux->ant;
    }
    free(aux);
   
    return lista;
}

Lista *listaFuncionario;

void incluir(){
    int _prontuario;
    string _nome;
    double _salario;
    cout << "==============================================" << endl;
    cout << "| INCLUSAO" << endl;
    cout << "==============================================" << endl;
    cout << "" << endl;
   
    cout << "==============================================" << endl;
    cout << "Insira o prontuario do Funcionario: " << endl;
    cin >> _prontuario;
    cout << "Insira o nome do Funcionario: " << endl;
    cin >> _nome;
    cout << "Insira o salario do Funcionario: " << endl;
    cin >> _salario;
    cout << "==============================================" << endl;
    S_Funcionario *funcionario = new S_Funcionario(_prontuario,_nome, _salario);

    Lista* pesquisa_funcionario = find(listaFuncionario, _prontuario);
   
   
    if (pesquisa_funcionario != NULL)
    {
        cout << "==============================================" << endl;
        cout << "ERRO: Usuario ja existente" << endl;
        cout << "==============================================" << endl;
        menu();
        return;
    }

    listaFuncionario = insert(listaFuncionario, funcionario);
    cout << "==============================================" << endl;
    cout << "SUCESSO: Usuario Cadastrado" << endl;
    cout << "==============================================" << endl;

    menu();
    return;
};

void excluir(){
    int _prontuario;
    cout << "==============================================" << endl;
    cout << "| EXCLUSAO " << endl;
    cout << "==============================================" << endl;
    cout << " " << endl;
    cout << "==============================================" << endl;
    cout << "Insira o prontuario do Funcionario: ";
    cin >> _prontuario;
    cout << "==============================================" << endl;

    Lista* pesquisa_funcionario = find(listaFuncionario, _prontuario);
   
    if (pesquisa_funcionario != NULL)
    {
        listaFuncionario = remove(listaFuncionario, _prontuario);
        cout << "==============================================" << endl;
        cout << "SUCESSO: Usuário Excluido" << endl;
        cout << "==============================================" << endl;
        menu();
        return;
    }
    cout << "==============================================" << endl;
    cout << "ERRO: Usuario inexistente" << endl;
    cout << "==============================================" << endl;
    menu();
    return;
}

void listar(){
    cout << "==============================================" << endl;
    cout << "| LISTAGEM" << endl;
    cout << "==============================================" << endl;
    cout << " " << endl;
    print(listaFuncionario);
    cout << "Pressione qualquer tecla para continuar...\n";
    getch();
    menu();
}

void pesquisar() {
    int _prontuario;
    cout << "==============================================" << endl;
    cout << "| PESQUISA" << endl;
    cout << "==============================================" << endl;
    cout << " " << endl;
    cout << "==============================================" << endl;
    cout << "Insira o prontuario do Funcionario: ";
    cin >> _prontuario;
    cout << "==============================================" << endl;

    Lista* pesquisa_funcionario = find(listaFuncionario, _prontuario);
   
    if (pesquisa_funcionario != NULL)
    {
        cout << "==============================================" << endl;
        cout << "-----------" << endl;
        cout << "Prontuario: " << pesquisa_funcionario->dado->getProntuario() << endl;
        cout << "Nome: " << pesquisa_funcionario->dado->getNome() << endl;
        cout << "Salario: (R$) " << pesquisa_funcionario->dado->getSalario() << endl;
        cout << "-----------" << endl;
        cout << "==============================================" << endl;
        cout << "Pressione qualquer tecla para continuar...\n";
        getch();
        menu();
        return;
    }
    cout << "==============================================" << endl;
    cout << "ERRO: Usuario inexistente" << endl;
    cout << "==============================================" << endl;
    menu();
    return;
}

void menu(){
    int _opcao = -1;
   
    cout << "==============================================" << endl;
    cout << "| MENU" << endl;
    cout << "==============================================" << endl;

    cout << "0. Sair" << endl;
    cout << "1. Incluir" << endl;
    cout << "2. Excluir" << endl;
    cout << "3. Pesquisar" << endl;
    cout << "4. Listar" << endl;
   
    cin >> _opcao;

    switch (_opcao)
    {
    case 0:
        exit(0);
        break;
    case 1:
        incluir();
        break;
    case 2:
        excluir();
        break;
    case 3:
        pesquisar();
        break;
    case 4:
        listar();
        break;
    default:
        break;
    }
}

void dbg_lista_funcionarios(){

    S_Funcionario* f1 = new S_Funcionario(002, "Paulo Moreira", 10.65);
    listaFuncionario = insert(listaFuncionario, f1);

    S_Funcionario* f2 = new S_Funcionario(003, "Ana Souza", 12.30);
    listaFuncionario = insert(listaFuncionario, f2);

    S_Funcionario* f3 = new S_Funcionario(004, "Carlos Lima", 9.80);
    listaFuncionario = insert(listaFuncionario, f3);

    S_Funcionario* f4 = new S_Funcionario(005, "Beatriz Silva", 11.50);
    listaFuncionario = insert(listaFuncionario, f4);

    S_Funcionario* f5 = new S_Funcionario(006, "João Pedro", 13.20);
    listaFuncionario = insert(listaFuncionario, f5);

}

int main(int argc, char* argv[])
{
    listaFuncionario = init();
    dbg_lista_funcionarios();
    cout << "==============================================" << endl;
    cout << "| Atividade Listas" << endl;
    cout << "==============================================" << endl;
    cout << "| Criado por: Alisson Santos" << endl;
    cout << "==============================================" << endl;
    cout << " " << endl;
   
    menu();

    return 0;
}