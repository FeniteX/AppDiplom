   M S B A M L   `   `   `  ����J  FDiplom_RepairPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Diplom_RepairPC.PagesDiplom_RepairPC.Pages  5      .    (Diplom_RepairPC.Pages.TypesComponentPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Diplom_RepairPC.Pages     5        N� Title$  TypesComponentPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      �� 5      $��70 �6   �� 5      $��70 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�5      .+�5      �� $*��$Сортировка по имени��6      TextBlockWhite#��[  63   �� 5      -   5         ��Name$ ComboBoxSortByName��5      .@�5      �� .��6   От А до Я6*   �� 5      .��6   От Я до А6*   5      �� 5      $&�� Поиск по названию��6   #��[  61   �� 5        BorderTextBox#��[ 6   ._�5      �� -   5         ��Name$ TextBoxSearch��5      61   5      5      &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ DataGridTypeComponent��5        $��1��6      AutoGenerateColumns .     
IsReadOnly .    Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Название��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *Names+4  .System.Windows.Controls.DataGridTemplateColumn  5$      $

 auto 6)      CellTemplate 5%      �� 5&      .R�5'      ��  ��Name$ BtnEdit��6(   -   5        $ ��Редактировать��5'   9     TextBlockUnderline#��[ 6N   5(   %   5)      5+        5,      $

 auto 6)    5-      �� 5.      .R�5/      �� $ BtnDel��6(   -   5        $��Удалить��5/   8   #��[ 6G   50   %   51      53      55        57   
   -   5        $ DataGridTypesComponent��57       $��1��6    .   . $y�Hidden=�58   %   $
��0.01(�69    59        5:      $	 ID��6%   $

 auto 61    6>   �� *IDs+  5;      $	 Название��6%   $

 auto 67    6D   �� *Names+5=      �� 5?   
     BorderButton#��[ 6   $��2��67   $��Right=�5@      ._�5A      �� -   5         ��Name$ BtnSaveExcel��5A      $��В Excel��6+   5B      5C      �� 5D   
   #��[ 6   $��3��67   $��Right=�5E      ._�5F      �� -   5        $ BtnAdd��5F      $��Добавить��6%   5G      5H      �� 5I   
   #��[ 6   $��3��67   $
��Left=�5J      ._�5K      �� -	   5        $ BtnBack��5K      $��
Назад��6&   5L      5M      5N      5O      