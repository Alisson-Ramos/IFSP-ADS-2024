#include <iostream>
using namespace std;

#define MAX 30

// Alisson Ramos
// Davi Coelho
struct PilhaVetor {
    int elementos[MAX];
    int topo;
};

PilhaVetor* initVetor() {
    PilhaVetor* p = new PilhaVetor();
    p->topo = 0;
    return p;
}

bool pushVetor(PilhaVetor* p, int valor) {
    if (p->topo >= MAX) return false;
    p->elementos[p->topo++] = valor;
    return true;
}

int popVetor(PilhaVetor* p) {
    if (p->topo == 0) return -1;
    return p->elementos[--p->topo];
}

bool isEmptyVetor(PilhaVetor* p) {
    return p->topo == 0;
}


struct Node {
    int valor;
    Node* prox;
};

struct PilhaLista {
    Node* topo;
};

PilhaLista* initLista() {
    PilhaLista* p = new PilhaLista();
    p->topo = nullptr;
    return p;
}

bool pushLista(PilhaLista* p, int valor) {
    Node* novo = new Node();
    novo->valor = valor;
    novo->prox = p->topo;
    p->topo = novo;
    return true;
}

int popLista(PilhaLista* p) {
    if (p->topo == nullptr) return -1;
    Node* temp = p->topo;
    int valor = temp->valor;
    p->topo = temp->prox;
    delete temp;
    return valor;
}

bool isEmptyLista(PilhaLista* p) {
    return p->topo == nullptr;
}


int main() {
    PilhaVetor* pilhaPares = initVetor();   
    PilhaLista* pilhaImpares = initLista();    

    int ultimo = -999999;

    cout << "Digite 30 numeros inteiros em ordem crescente:\n";
    for (int i = 0; i < MAX; i++) {
        int numero;
        do {
            cout << "Numero " << (i + 1) << ": ";
            cin >> numero;
            if (numero <= ultimo) {
                cout << "O numero deve ser maior que o anterior!\n";
            }
        } while (numero <= ultimo);
        ultimo = numero;

        if (numero % 2 == 0) {
            pushVetor(pilhaPares, numero);
        } else {
            pushLista(pilhaImpares, numero);
        }
    }

    cout << "\n--- Desempilhando Pares (vetor) em ordem decrescente ---\n";
    while (!isEmptyVetor(pilhaPares)) {
        cout << popVetor(pilhaPares) << " ";
    }

    cout << "\n--- Desempilhando Impares (lista encadeada) em ordem decrescente ---\n";
    while (!isEmptyLista(pilhaImpares)) {
        cout << popLista(pilhaImpares) << " ";
    }

    cout << "\n";

    return 0;
}
