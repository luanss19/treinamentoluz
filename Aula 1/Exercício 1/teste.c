#include <stdio.h>
#include <stdlib.h>

int main(){

    int *v1 = (int*)malloc(10*sizeof(int));
    int *v2 = (int*)calloc(10, sizeof(int));

    int i;
    for(i=0;i<10;i++){
        printf(" %d ", v1[i]);
    }
    printf("\n");
    for(i=0;i<10;i++){
        printf(" %d ", v2[i]);
    }



    //Implicita
    int a = 3.1415; // 3
    float b = 3; //3.0

    //Explicita
    int valor = 5;

    printf("\n\n Valor em float = %f\n", (float)valor);


    return 0;
}