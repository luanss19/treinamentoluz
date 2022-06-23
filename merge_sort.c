#include <stdio.h>
#include <stdlib.h>

// Cria dois subarrays de arr[] e junta os mesmos.
// Primeiro subarray e arr[l..m]
// Segundo subarray e arr[m+1..r]
void merge(int arr[], int l, int m, int r)
{
	int i, j, k;
	int n1 = m - l + 1;
	int n2 = r - m;

	/* cria arrays temporarios */
	int L[n1], R[n2];

	/* poe os dados nos arrays temporarios L[] and R[] */
	for (i = 0; i < n1; i++)
		L[i] = arr[l + i];
	for (j = 0; j < n2; j++)
		R[j] = arr[m + 1 + j];

	/* Junta os arrays temporarios no array arr[l..r]*/
	i = 0; // index inicial do primeiro subarray
	j = 0; // index inicial do segundo subarray
	k = l; // index inicial do subarray que foi unido
	while (i < n1 && j < n2) {
		if (L[i] <= R[j]) {
			arr[k] = L[i];
			i++;
		}
		else {
			arr[k] = R[j];
			j++;
		}
		k++;
	}

	/* Copia os elementos do L[] se tiver sobrado*/
	while (i < n1) {
		arr[k] = L[i];
		i++;
		k++;
	}

	/* Copia os elementos do R[] se tiver sobrado*/
	while (j < n2) {
		arr[k] = R[j];
		j++;
		k++;
	}
}

void mergeSort(int arr[], int l, int r)
{
	if (l < r) {
		//calcula o meio do vetor
		int m = l + (r - l) / 2;

		// faz o sort da primeira e segunda parte
		mergeSort(arr, l, m);
		mergeSort(arr, m + 1, r);

		merge(arr, l, m, r);
	}
}

int main()
{

    int size;
    printf("Digite o tamanho da lista: \n");
    scanf("%d", &size);
    int arr[size], i, cont, aux;
    for (i = 0; i < size; i++)
        arr[i] = rand() % size;
    printf("\nElementos do array sem serem ordenados:");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);
    }
    
	mergeSort(arr, 0, size - 1);


    printf("\n\nElementos do array em ordem crescente:\n");
    for (i = 0; i < size; i++)
    {
        printf("[%d]: %d  ", i + 1, arr[i]);

    }
}
