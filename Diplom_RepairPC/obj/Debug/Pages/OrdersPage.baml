   M S B A M L   `   `   `  ����J  FDiplom_RepairPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Diplom_RepairPC.PagesDiplom_RepairPC.Pages  5      &     Diplom_RepairPC.Pages.OrdersPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Diplom_RepairPC.Pages     5        N� Title$  
OrdersPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      �� 5      $��70 �6   �� 5      $��70 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Center=�5      .+�5      �� $(��"Сортировка по цене��6      TextBlockWhite#��[  62   �� 5      -   5         ��Name$ ComboBoxSortByCost��5      .@�5      �� .��6   По возрастанию6/   �� 5      .��6   По убыванию6,   5      �� 5      $G��AПоиск по ФИО сотрудника или клиента��6   #��[  6C   �� 5        BorderTextBox#��[ 6   ._�5      �� -   5         ��Name$ TextBoxSearch��5      61   5      5      &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ DataGridOrder��5        $��1��6      AutoGenerateColumns .     
IsReadOnly .    Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Клиент��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *Diplom_Client.FIO+  5$      $	 Сотрудник��6%   $

 auto 68    6E   �� *Diplom_Employee.FIO+  5%      $	 Стоимоcть��6%   $

 auto 68    6E   �� *	CostOrder+  5&      $	 Дата начала��6%   $

 auto 6:    6G   �� *	DateStart+  5'      $!	 Дата окончания��6%   $

 auto 6=    6J   �� *	DateEnd+  5(      $	 Статус��6%   $

 auto 65    6B   �� *Status+4  .System.Windows.Controls.DataGridTemplateColumn  5)      $

 auto 6)      CellTemplate 5*      �� 5+      .R�5,      ��  ��Name$ BtnDetalied��6(   -   5        $��Подробнее��5,   =     TextBlockUnderline#��[ 6N   5-   (   5.      50        51      $

 auto 6)    52      �� 53      .R�54      �� $ BtnEdit��6(   -   5        $ ��Редактировать��54   9   #��[ 6N   55   %   56      58        59      $

 auto 6)    5:      �� 5;      .R�5<      �� $ BtnDel��6(   -   5        $��Удалить��5<   8   #��[ 6G   5=   %   5>      5@      5B        5D   
   -   5        $ DataGridOrders��5D       $��1��6    .   . $y�Hidden=�5E   %   $
��0.01(�69    5F        5G      $	 Клиент��6%   $

 auto 65    6B   �� *Diplom_Client.FIO+  5H      $	 Сотрудник��6%   $

 auto 68    6E   �� *Diplom_Employee.FIO+  5I      $	 Стоимоcть��6%   $

 auto 68    6E   �� *Cost+  5J      $(	 "Дата начала заказа��6%   $

 auto 6A    6N   �� *DateOrderStart+  5K      $.	 (Дата окончания заказа��6%   $

 auto 6D    6Q   �� *DateOrderEnd+  5L      $	 Статус��6%   $

 auto 65    6B   �� *Status+5N      �� 5P   
     BorderButton#��[ 6   $��2��67   $��Right=�5Q      ._�5R      �� -   5         ��Name$ BtnSaveExcel��5R      $��В Excel��6+   5S      5T      �� 5U   
   #��[ 6   $��3��67   $��Right=�5V      ._�5W      �� -	   5        $ BtnAdd��5W      $��Добавить��6%   5X      5Y      �� 5Z   
   #��[ 6   $��3��67   $
��Left=�5[      ._�5\      �� -
   5        $ BtnBack��5\      $��
Назад��6&   5]      5^      5_      5`      