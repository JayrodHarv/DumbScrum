   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DumbScrum	DumbScrum  5   	   1clr-namespace:DumbScrum.ViewsDumbScrum.Views  5   	       DumbScrum.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DumbScrum     5   	   /viewsclr-namespace:DumbScrum.Views     5   	   -   5          � Title$  
Dumb Scrum��5	   	   $	��650��5
   	   $
��1200��6   
������� � WindowStyle$ SingleBorderWindow=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � ���	#FFE6E6E6 � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      �� 5      $��25 �6   .+�5   
   � $��0��6   $��0��6    � ColumnDefinitions 5      �� 5      $��60 �6#   �� 5      �� 5      $	��150 �6#   .+�5      �� $��0��6   ���	#FF3A3A3A �� Orientation$ 
Horizontal=�6@   .+�5      �� $��DS��6   
�������$��16(�69   $
��Bold"�6G   $��Center=�6Y    �� Padding$ 5,0,0,0q�6t   �� 5      -   5         ��Name$ imgLogo��5      $��35��6)   $��35��64   5      o� 5      $��1��6   ���	#FF3A3A3A.@�5       m� -   5         m�Name$ mnuHome��5       $
��Home��6,   
�������$��40��6M   $��Center=�6Y   6t   m� 5!      -   5        $ mnuMyProjects��5!      $��My Projects��62   
�������$��40��6Z   6f   m� 5"      -   5        $ mnuBrowseProjects��5"      $��Browse Projects��66   
�������$��40��6b   6n   5#      � 5$      -   5        	 �Name$	 userBox��5$      $��2��6$   ���	#FF3A3A3A
 � ToolTip$ 
 Click to view/edit profile��6i    5%      �� 5&      �� 5'      $��35 �6'   .+�5)      �� -   5         ��Name$ txtDisplayName��5)      
�������$��Center=�6q   $��Right=�6�   $ 5q�6�   ��64   �� *LoggedInUser.DisplayName+6�   A� 5*      $��1��6   $��35��6*   $��35��66   ��5+      �� 5,      -   5        
imgPfp��5,   %   5.      5/      50      �� 51   
   $��1��6   ��6&   �� *CurrentView+53      54      55      