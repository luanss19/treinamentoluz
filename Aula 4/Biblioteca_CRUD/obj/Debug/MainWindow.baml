   M S B A M L   `   `   `  ����J  FBiblioteca_CRUD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Biblioteca_CRUDBiblioteca_CRUD  5   	        Biblioteca_CRUD.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Biblioteca_CRUD     5   	     � Title$  
Biblioteca��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      .+�5   
   �� $
��Novo��6   $
��Left=�68   $��	10,10,0,0q�6S   $	��Top=�6f   $��20��6~   $��33��6�   ��6    �� *Add+6�   �� 5   
   $��Remover��6   $
��Left=�6>   $��
107,10,0,0q�6Y   $	��Top=�6m   $��54��6�   $��20��6�   ��6#   �� *Remove+6�   �� 5   
   -   5        $��Editar��5      $
��Left=�6;   $��	48,10,0,0q�6V   $	��Top=�6i   $��54��6�   $��20��6�   ��6"   �� *Edit+6�   �� 5   
   $��10,35,125,10q�6�   ��6>   �� *listaLivros+ �� SelectedItem 6b   �� *LivroSelecionado+ �� View 5      �� 5      .P�5      ��  �� Width$
 Auto��6%   $
��Nome��62    �� DisplayMemberBinding 6@   �� *Name+�� 5      $
 Auto��6%   $��Autor��62    6A   �� *Author+�� 5      $
 Auto��6%   $��Nº de Páginas��62    6I   �� *Pages+5      5      �� 5   
   $
��Left=�6   $��
517,17,0,0q�6/   $
��Wrap=�6C   $"��Selecione o Banco de Dados: ��6W   $	��Top=�6{   $	��158��6�   �� 5   
   $��680,12,10,10q�6;   ��6   �� *DataButtons+��5      �� 5      .�5      ��  �� Property � �� �� Value$ 5q�6@   5      ��5!      �� 5"      .R�5#      �� $
��Left=�5%      $	��Top=�60   ��5#      �� *
ButtonName+��5$      �� *ButtonCommand+5%   0   5&      5(      5)      5*      