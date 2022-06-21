#include <stdio.h>
#include <stdlib.h>

int particiona(int arr[], int start, int end){
    int pivo = arr[start];
    int i, j;
    i = start;
    j = end;
    int aux;
    while(i <= j){
        while(arr[i] <= pivo) if(i <= j)i++;else break; 
        while(arr[j] > pivo)  if(j >= i)j--;else break; 
        if(i <= j){
            aux = arr[i];
            arr[i] = arr[j];
            arr[j] = aux;
            i++; j--;
        }
    }
    arr[start] = arr[j];
    arr[j] = pivo;
    return j;
    
}

void quicksort(int arr[], int start, int end)
{
    int pivo;
	if (end > start) {
		pivo = particiona(arr, start, end);
        quicksort(arr, start, pivo-1);
        quicksort(arr, pivo+1, end);

	}
}



void main()
{
    int size;
    printf("Digite o tamanho da lista: \n");
    scanf("%d", &size);
    int arr[size], i, cont, aux;
    for (i = 0; i < size; i++)
        arr[i] = rand() % size;
    printf("\nElementos do array sem serem ordenados:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);
    }
    
    quicksort(arr, 0, size - 1);


    printf("\n\nElementos do array em ordem crescente:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);

    }
}
