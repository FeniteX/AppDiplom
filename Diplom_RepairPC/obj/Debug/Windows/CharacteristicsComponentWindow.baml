   M S B A M L   `   `   `  ����J  FDiplom_RepairPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Diplom_RepairPC.WindowsDiplom_RepairPC.Windows  5   	   <    6Diplom_RepairPC.Windows.CharacteristicsComponentWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Diplom_RepairPC.Windows     5   	   -   5          � Title$?  9Характеристики комплектующего��5   	   $
��1080��6/   $
��1920��6=   $
��1080��6J   $
��1920��6[   ��5
      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFB795DA��6    � Offset$ 1Q�6-   � 5      $ 	#FF6722AF��6   5      .��5      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�6,   $��Center=�6I   .+�5      �� -   5         ��Name$ ListViewComponent��5      $��0q�62   $��Disabled=�5      $��1��6I   ��5   F   Z� ��5      �� 5      .R�5      �  � Orientation$ 
Horizontal=�6$   $��Center=�5       5      ��5      �� 5      .R�5      ��  �� CornerRadius$ 15��6!   	 �� Padding$	 10q�63   ���	#FFF4EDFF$��5q�5      ._�5      � .+�5    "   �� $ 
Horizontal=�6-   $��Center=�5!   -   $��Center=�5"   -   .+�5#   &   � 
 � RowDefinitions
 5$   *   �� 5%   .   �� 5&   .   .+�5)   *   �� $��Название:��64   $	��Top=�6E   �� 5*   *   $��1��6J   $	��Top=�6W   ��64   �� *Name+6W   5+   '   � 5,   &   
 5-   *   �� 5.   .   �� 5/   .   .+�52   *   �� $#��Характеристики:��64   $	��Top=�6K   �� 53   *   $��1��6U   $	��Top=�6b   $��18(�6z   ��64   �� *Characteristics+6z   54   '   55   #   56      57      58      5:      �� 5<         BorderButton#��[  6   ._�5=      �� -   5         ��Name$ 	BtnCancel��5=      $��
Назад��6,   5>      5?      5@      5A      5B      