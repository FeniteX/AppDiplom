   M S B A M L   `   `   `  ����J  FDiplom_RepairPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Diplom_RepairPC.PagesDiplom_RepairPC.Pages  5      )    #Diplom_RepairPC.Pages.TypesWorkPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Diplom_RepairPC.Pages     5        N� Title$  TypesWorkPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      �� 5      $��70 �6   �� 5      $��70 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�5      .+�5      �� $2��,Сортировка по стоимости��6      TextBlockWhite#��[  67   �� 5      -   5         ��Name$ ComboBoxSortByCost��5      .@�5      �� .��6   По возрастанию6/   �� 5      .��6   По убыванию6,   5      �� 5      $&�� Поиск по названию��6   #��[  61   �� 5        BorderTextBox#��[ 6   ._�5      �� -   5         ��Name$ TextBoxSearch��5      61   5      5      &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ DataGridTypeWork��5        $��1��6      AutoGenerateColumns .     
IsReadOnly .    Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Название��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Name+  5$      $	 Стоимость��6%   $

 auto 68    6E   �� *
CostWork+  5%      $	 Время работы��6%   $

 auto 6;    6H   �� *
WorkTime+4  .System.Windows.Controls.DataGridTemplateColumn  5&      $

 auto 6)      CellTemplate 5'      �� 5(      .R�5)      ��  ��Name$ BtnEdit��6(   -   5        $ ��Редактировать��5)   9     TextBlockUnderline#��[ 6N   5*   %   5+      5-        5.      $

 auto 6)    5/      �� 50      .R�51      �� $ BtnDel��6(   -   5        $��Удалить��51   8   #��[ 6G   52   %   53      55      57        59   
   -   5        $ DataGridTypesWork��59       $��1��6    .   . $y�Hidden=�5:   %   $
��0.01(�69    5;        5<      $	 ID��6%   $

 auto 61    6>   �� *ID+  5=      $	 Название��6%   $

 auto 67    6D   �� *Name+  5>      $	 Описание��6%   $

 auto 67    6D   �� *Description+  5?      $	 Стоимость��6%   $

 auto 68    6E   �� *
CostWork+  5@      $	 Время работы��6%   $

 auto 6;    6H   �� *
WorkTime+5B      �� 5D   
     BorderButton#��[ 6   $��2��67   $��Right=�5E      ._�5F      �� -   5         ��Name$ BtnSaveExcel��5F      $��В Excel��6+   5G      5H      �� 5I   
   #��[ 6   $��3��67   $��Right=�5J      ._�5K      �� -   5        $ BtnAdd��5K      $��Добавить��6%   5L      5M      �� 5N   
   #��[ 6   $��3��67   $
��Left=�5O      ._�5P      �� -	   5        $ BtnBack��5P      $��
Назад��6&   5Q      5R      5S      5T      