   M S B A M L   `   `   `  ����  
nature-net%    nature_net.user_controls.signup   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  5      .��5
      ]� ��. 
�������.+�5   
   �� -   5        $
��desc��5      $	��Top=�6    $��Center=�65   
���  ��6f   �� 5   
     �� Orientation$  
Horizontal=�6   $	��Top=�6.   $	��250��6C   .+�5      ]� ��. $	��160��6-   .+�5      �� -   5        $��
label_name��5      $	��Top=�6*   $��14(�6?   .��6M   Name:6T   <  6Microsoft.Surface.Presentation.Controls.SurfaceTextBox  5      -   5        $��textbox_name��5   #   $	��Top=�67   $	��150��6L   $��14(�6X   $��38��6f   $��Center=�6r   
�������
�������$��2q�5   K   
���   �6t   �� 5      -   5        $��label_email��5      $	��Top=�6+   $��14(�6@   .��6N   Email:6V     5      -   5        $��textbox_email��5   #   $	��Top=�68   $	��150��6M   
�������$��2q�6q   $��14(�5      $��38��6(   
�������
���   �$��Center=�6`   .��6�   5      �� 5      -   5        $��label_password��5      $	��Top=�6.   $��14(�6C   .��6Q   	Password:6\   @  :Microsoft.Surface.Presentation.Controls.SurfacePasswordBox  5      -   5        $��textbox_password��5   '   $	��Top=�6?   $	��150��6T   $��Segoe UI)�6`   $��Center=�6v   
���   �$��14(�5   ;   
�������
�������$��2q�6z   5      =  7Microsoft.Surface.Presentation.Controls.SurfaceCheckBox  5      -   5        $��checkbox_agreement��5   $   $	��Top=�6>   $��5,5,5,5q�6S   $��42��6d   
�������
���   �$��3q�5   L   .��5      �� $��Segoe UI)�6    $��12(�66   
���   �$
��Wrap=�6W   !I Agree to Terms and Conditions5      5      5      �� 5       -	   5        $	��why��5       $
��Wrap=�6#   $	��140��67    �� Padding$ 
5,10,10,10q�6C    �� TextAlignment$ Justify=�6X   
���   ���The NatureNet project is a citizen science project that incorporates crowdsourced interaction design. Participants can collect and analyse scientific data using their mobile computing devices and a multi-touch tabletop, and they can also participate in the re-design of the interface by which their scientific contributions are made. The role of the crowd in Naturenet is extended to include interaction design in addition to data collection and analysis.5"      5#      �� 5$   
   $  Vertical=�6   $	��Top=�6,   .+�5%      �� -
   5        $��label_choose_avatar��5%      $��14(�6/   .��6=   Choose an Avatar:6P   <  6Microsoft.Surface.Presentation.Controls.SurfaceListBox  5&      -   5        $��listbox_avatars��5&      $��5,0,5,0q�66   $	��125��6G   ��. $��Hidden=�5'   ;   $��Hidden=�5(      ��5)      �� 5*      .R�5+      A�  A� Columns 6&   ��  �� Path$ Items.Count� �� RelativeSource ��  �� AncestorType" ��5,       � DataContext 5.      �� 5/      .R�50      �� $��Center=�6?   $��Center=�6\   ��6    �� *ImageSource+6\   51      53      ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton  54      -   5        $��button_submit��54      
��� � �$	��100��6F   
�������$��Center=�55      $��Center=�6@   $��Right=�56      $��0,5,5,0q�6:   .��6K   Submit6S   57      58      5:      