   M S B A M L   `   `   `  ����J  FDiplom_RepairPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Diplom_RepairPC.PagesDiplom_RepairPC.Pages  5      *    $Diplom_RepairPC.Pages.ComponentsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Diplom_RepairPC.Pages     5        N� Title$  ComponentsPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      �� 5      $��70 �6   �� 5      $��70 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�5      .+�5      �� $(��"Сортировка по цене��6      TextBlockWhite#��[  62   �� 5      -   5         ��Name$ ComboBoxSortByCost��5      .@�5      �� .��6   По возрастанию6/   �� 5      .��6   По убыванию6,   5      �� 5      $��Фильтрация��6   #��[  6*   �� 5      -   5        $# ComboBoxFilterByTypeComponent��5       �� DisplayMemberPath$
 Name��5      �� 5      $&�� Поиск по названию��6   #��[  61   �� 5        BorderTextBox#��[ 6   ._�5       �� -   5         ��Name$ TextBoxSearch��5       61   5!      5"      &   System.Windows.Controls.DataGrid  5$   
   -   5          Name$ DataGridComponent��5$       $��1��6      AutoGenerateColumns .     
IsReadOnly . 	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Название��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Name+4  .System.Windows.Controls.DataGridTemplateColumn  5(      $
 auto 6)   $"
 Характеристики��66      CellTemplate 5)      �� 5*      .R�5+      ��  ��Name$ BtnCharacteristics��6(   -   5        $��Подробнее��5+   D     TextBlockUnderline#��[ 6U   5,   %   5-      5/        50      $
 Стоимоcть��6%   $
 auto 68    6E   �� *
CostItem+  51      $)
 #Вид комплектующего��6%   $
 auto 6A      FontSize$ 20Q�6N    52   %   �� *Diplom_TypeComponent.Name+  53      $
 auto 6)    54      �� 55      .R�56      �� $ BtnEdit��6(   -   5        $ ��Редактировать��56   9   #��[ 6N   57   %   58      5:        5;      $
 auto 6)    5<      �� 5=      .R�5>      �� $ BtnDel��6(   -   5        $��Удалить��5>   8   #��[ 6G   5?   %   5@      5B      5D        5F   
   -   5        $ DataGridComponents��5F       $��1��6    .   . $y�Hidden=�5G   %   $
��0.01(�69   	 5H        5I      $
 ID��6%   $
 auto 61    6>   �� *ID+  5J      $
 Название��6%   $
 auto 67    6D   �� *Name+  5K      $"
 Характеристики��6%   $	 400 6=    6I   �� *Characteristics+  5L      $
 Стоимоcть��6%   $
 auto 68    6E   �� *
CostItem+  5M      $)
 #Вид комплектующего��6%   $
 auto 6A    5N   %   �� *Diplom_TypeComponent.Name+5P      �� 5R   
     BorderButton#��[ 6   $��2��67   $��Right=�5S      ._�5T      �� -	   5         ��Name$ BtnSaveExcel��5T      $��В Excel��6+   5U      5V      �� 5W   
   #��[ 6   $��3��67   $��Right=�5X      ._�5Y      �� -
   5        $ BtnAdd��5Y      $��Добавить��6%   5Z      5[      �� 5\   
   #��[ 6   $��3��67   $
��Left=�5]      ._�5^      �� -   5        $ BtnBack��5^      $��
Назад��6&   5_      5`      5a      5b      