   M S B A M L   `   `   `  ����J  FDiplom_RepairPC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Diplom_RepairPC.WindowsDiplom_RepairPC.Windows  5   	   +    %Diplom_RepairPC.Windows.OrderDetalied   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Diplom_RepairPC.Windows     5   	   -   5          � Title$  Title��5   	   $	��700��6   $
��1500��6$   $	��700��61   $
��1500��6A   ��5
      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ 	#FFB795DA��6    � Offset$ 1Q�6-   � 5      $ 	#FF6722AF��6   5      .��5      ��  �� Orientation$ Vertical=�6   $��Center=�6(   $��Center=�6E   .+�5   
   �  � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��Услуги��6      TextBlockWhite#��[  6&   �� 5      -   5         ��Name$ TextBlockComponent��5   e   $ ��Комплектующие��6   #��[  6-   $��1��6U   6e   5      �� 5   
   $ 
Horizontal=�6   $��Center=�6.   $��Center=�6K   .+�5      �� -   5         ��Name$ ListViewOrderTypeWork��5      $��0q�66   $��Disabled=�5      $	��750��6I   ��5   J   Z� ��5       �� 5!      .R�5"      � 	 � Orientation$	 
Horizontal=�6$   $��Center=�5#       5$      ��5&      �� 5'      .R�5(      �� 
 �� CornerRadius$
 15��6!    �� Padding$ 10q�63   ���	#FFF4EDFF$��5q�5)      $	��700��6(   ._�5*      � .+�5+   "   �� $ Vertical=�6-   $��Center=�5,   -   $��Center=�5-   -   .+�5.   &   �  5/   *   �� 50   .   �� 51   .   .+�54   *   �� $��Название:��64   $
��Left=�6E   $	��120��6`   �� 55   *   $��1��6Z   $
��Left=�6j   ��64   �� *Diplom_TypeWork.Name+6j   56   '   � 57   &    58   *   �� 59   .   �� 5:   .   .+�5=   *   �� $��Стоимость:��64   $
��Left=�6F   $	��120��6a   �� 5>   *   $��1��6^   $
��Left=�6n   ��64   �� *Diplom_TypeWork.CostWork+6n   5?   '   5@   #   5A      5B      5C      5E      �� 5G      -   5        $ ListViewOrderComponent��5G      $��0q�67   $��Disabled=�5H      $	��750��6I   ��5G   K   Z� ��5I      �� 5J      .R�5K      � $	 
Horizontal=�6$   $��Center=�5L       5M      ��5O      �� 5P      .R�5Q      �� $
 15��6!   $ 10q�63   ���	#FFF4EDFF$��5q�5R      $	��700��6(   ._�5S      � .+�5T   "   �� $ Vertical=�6-   $��Center=�5U   -   $��Center=�5V   -   .+�5W   &   �  5X   *   �� 5Y   .   �� 5Z   .   .+�5]   *   �� $��Название:��64   $
��Left=�6E   $	��120��6`   �� 5^   *   $��1��6[   $
��Left=�6k   ��64   �� *Diplom_Component.Name+6k   5_   '   � 5`   &    5a   *   �� 5b   .   �� 5c   .   .+�5f   *   �� $��Стоимость:��64   $
��Left=�6F   $	��120��6a   �� 5g   *   $��1��6_   $
��Left=�6o   ��64   �� *Diplom_Component.CostItem+6o   5h   '   5i   #   5j      5k      5l      5n      5o      �� 5q   
     BorderButton#��[ 6   ._�5r      �� -   5         ��Name$ 	BtnCancel��5r      $��
Назад��6(   5s      5t      5u      5v      