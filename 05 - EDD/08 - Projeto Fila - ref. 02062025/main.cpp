// fila_atendimento.cpp
// Sistema simples de atendimento
// Implementa duas filas: vetor (senhasGeradas) e ponteiro (senhasAtendidas)
// Alisson Ramos – Junho/2025

#include <iostream>
using namespace std;

// --- Fila baseada em vetor circular ---
const int MAX = 100; // capacidade máxima da fila de senhas geradas
struct FilaVetor {
    int dados[MAX];
    int inicio;
    int fim;
    int qtd;
};

FilaVetor* initFilaVetor() {
    auto* f = new FilaVetor();
    f->inicio = 0;
    f->fim = 0;
    f->qtd = 0;
    return f;
}

bool isFullVetor(FilaVetor* f) {
    return f->qtd == MAX;
}

bool isEmptyVetor(FilaVetor* f) {
    return f->qtd == 0;
}

bool enfileirarVetor(FilaVetor* f, int valor) {
    if (isFullVetor(f)) return false;
    f->dados[f->fim] = valor;
    f->fim = (f->fim + 1) % MAX;
    f->qtd++;
    return true;
}

int desenfileirarVetor(FilaVetor* f) {
    if (isEmptyVetor(f)) return -1;
    int valor = f->dados[f->inicio];
    f->inicio = (f->inicio + 1) % MAX;
    f->qtd--;
    return valor;
}

int tamanhoVetor(FilaVetor* f) {
    return f->qtd;
}

// --- Fila baseada em ponteiros (lista encadeada) ---
struct Node {
    int valor;
    Node* prox;
};

struct FilaPtr {
    Node* inicio;
    Node* fim;
    int qtd;
};

FilaPtr* initFilaPtr() {
    auto* f = new FilaPtr();
    f->inicio = f->fim = nullptr;
    f->qtd = 0;
    return f;
}

bool isEmptyPtr(FilaPtr* f) {
    return f->inicio == nullptr;
}

bool enfileirarPtr(FilaPtr* f, int valor) {
    auto* novo = new Node();
    novo->valor = valor;
    novo->prox = nullptr;
    if (isEmptyPtr(f)) {
        f->inicio = f->fim = novo;
    } else {
        f->fim->prox = novo;
        f->fim = novo;
    }
    f->qtd++;
    return true;
}

int desenfileirarPtr(FilaPtr* f) {
    if (isEmptyPtr(f)) return -1;
    Node* temp = f->inicio;
    int valor = temp->valor;
    f->inicio = temp->prox;
    if (f->inicio == nullptr) f->fim = nullptr;
    delete temp;
    f->qtd--;
    return valor;
}

int tamanhoPtr(FilaPtr* f) {
    return f->qtd;
}

int main() {
    FilaVetor* senhasGeradas = initFilaVetor();   // fila de espera (vetor)
    FilaPtr*   senhasAtendidas = initFilaPtr();   // histórico de atendimentos (lista)

    int proximaSenha = 1;
    int opcao;

    do {
        cout << "\n=== Sistema de Atendimento ===\n";
        cout << "Senhas na fila de espera: " << tamanhoVetor(senhasGeradas) << "\n";
        cout << "0. Sair\n1. Gerar senha\n2. Realizar atendimento\nEscolha: ";
        cin >> opcao;

        switch (opcao) {
            case 1: {
                if (enfileirarVetor(senhasGeradas, proximaSenha)) {
                    cout << "Senha gerada: " << proximaSenha << "\n";
                    proximaSenha++;
                } else {
                    cout << "Fila cheia! N\xE3o foi poss\xEDvel gerar nova senha.\n";
                }
                break;
            }
            case 2: {
                if (!isEmptyVetor(senhasGeradas)) {
                    int senha = desenfileirarVetor(senhasGeradas);
                    enfileirarPtr(senhasAtendidas, senha);
                    cout << "Atendendo senha: " << senha << "\n";
                } else {
                    cout << "Nenhuma senha para atender.\n";
                }
                break;
            }
            case 0: {
                if (!isEmptyVetor(senhasGeradas)) {
                    cout << "Ainda restam " << tamanhoVetor(senhasGeradas) << " senha(s) a atender. Finalize o atendimento antes de sair.\n";
                    opcao = -1; // para continuar o loop
                } else {
                    cout << "Encerrando sistema. Total de senhas atendidas: " << tamanhoPtr(senhasAtendidas) << "\n";
                }
                break;
            }
            default:
                cout << "Op\xE7\xE3o inv\xE1lida!\n";
        }
    } while (opcao != 0);

    return 0;
}
