   M S B A M L   `   `   `  ����E  Anature-net, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:nature_net.user_controlsnature_net.user_controls  5      *    $nature_net.user_controls.tab_control   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5uc&clr-namespace:nature_net.user_controls     5      .��5
      �� -   5        $	��tab��5
      $
��Left=�6     �� TabStripPlacement$  Right=�62   
������ $��0,0,0,0q�6e   
��� d � � 	Resources 5   
    �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers ]� 
Conditions	 �� Property
 �� Value �� 
TargetName P� Property P� Value��%�  '��       �� 5      " ��6   .�5      ��  � �� 5      �� 5      " ��6*   .R�5      �� $��Border��6%   
��� d �._�5   "   �� $ ContentSite��63   $	��200��6H   $��45��6U   $��Center=�5   )   $��Center=�5   )   $��Header��5   )   5       5      ]� 5   "    5   &   �� 5   *   	 �@��
IsSelected
 . 6J   �� 5   *   	 �@��TabStripPlacement$
 Right=�6Q   ./�5   &   �� $ Border��6-    � ��$ 0,0,2,2q�6\   5   #   ]� 5   "    5    &   �� 5!   *   	 �@��
IsSelected
 .  6J   �� 5"   *   	 �@��TabStripPlacement$
 Right=�6Q   ./�5$   &   �� $ Border��6-    � ��$ 2,0,2,2q�6\   5%   #   ]� 5&   "    5'   &   �� 5(   *   	 �@��
IsSelected
 . 6J   �� 5)   *   	 �@��TabStripPlacement$

 Left=�6Q   ./�5+   &   �� $ Border��6-    � ��$ 2,0,0,2q�6\   5,   #   ]� 5-   "    5.   &   �� 5/   *   	 �@��
IsSelected
 .  6J   �� 50   *   	 �@��TabStripPlacement$

 Left=�6Q   ./�52   &   �� $ Border��6-    � ��$ 2,0,2,2q�6\   53   #   P� 54   "    �@��
IsSelected . . �55   &   �� $ Border��6-    � ��
 ����6W   56   #   P� 57   "    �@��
IsSelected .  . �58   &   �� $ Border��6-    � ��
 �����6W   59   #   P� 5:   "    �@��TabStripPlacement$ Right=�6G   . �5;   &   �� $ Border��6-    �@��CornerRadius$ 0,20,5,0��6Y   �� 5<   &   $ Border��6-    � ��$ -2,0,0,0q�6S   5=   #   P� 5>   "    �@��TabStripPlacement$
 Left=�6G   . �5?   &   �� $ Border��6-    �@��CornerRadius$ 20,0,0,5��6Y   �� 5@   &   $ Border��6-    � ��$ 0,0,-2,0q�6S   5A   #   5C      5E      5F      5G      .@�5I   
   �� -   5        $��	users_tab��5I      ��5J      �� 5K      $��3q�6   $��Segoe UI)�6'   $��14(�6=    �� TextAlignment$ Center=�5L      $��Center=�63   $��Users��6N   $��Center=�5M       � LayoutTransform 5N      �� 5O       �� CenterX$ 0Q�6*    �� CenterY$ 0Q�66    �� Angle$ 90Q�6B   5Q      .��5S      ,   &nature_net.user_controls.users_listbox  -   5          Name$ users_listbox��5S      $	��220��66   5T      5U      �� 5V   
   -   5        $��activities_tab��5V      ��5W      �� 5X      $��3q�6   $��Segoe UI)�6'   $��14(�6=   $ Center=�5Y      $��Center=�64   $��
Activities��6O   $��Center=�5Z       5[      �� 5\      $ 0Q�6*   $ 0Q�66   $ 90Q�6B   5^      .��5`      -   'nature_net.user_controls.custom_listbox  -   5          Name$ activities_listbox��5`       $	��220��6<   5a      �� 5b   
   -   5        $��design_ideas_tab��5b      ��5c      �� 5d      $��3q�6   $��Segoe UI)�6'   $��14(�6=   $ Center=�5e      $��Center=�63   $��Design Ideas��6N   $��Center=�5f       5g      �� 5h      $ 0Q�6*   $ 0Q�66   $ 90Q�6B   5j      .��5l      3   -nature_net.user_controls.design_ideas_listbox  -   5          Name$ design_ideas_listbox��5l   &   $	��220��6D   5m      5n      5o      5q      