   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:DumbScrum.ToolWindowsDumbScrum.ToolWindows  5   	   3    -DumbScrum.ToolWindows.CreateNewTaskFileWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:DumbScrum.ToolWindows     5   	   -   5          � Title$  Create New File��5   	   $	��300��6!   $	��500��6.    � WindowStyle$ 
ToolWindow=�6Q    � WindowStartupLocation$ CenterScreen=�6j   .��5	      �  � RowDefinitions 5
   
   �� 5      $��15 �6   �� 5      $��50 �6   �� 5      �� 5      $��50 �6   .+�5   
   �� $��1��6   $��2��6!   $
��Wrap=�65    �� TextAlignment$
 Left=�5      $��14(�6)   $��10,0,0,0q�67   $��SemiBold"�6I   (&Create New File Using Project Template5      �� 5   
   $��2��6   $��2��6"   .+�5      �� $��
File Name:��6   $��DemiBold"�5      $��10, 0q�5      $��Center=�5      �� 5       �� Orientation$ 
Horizontal=�6   $
��10,0q�62   $��Center=�6@   .+�5      �� -   5         ��Name$ tbFilePrefix��5      $��Center=�5      $��	4.1.1-UC-��5      �� 5      -   5         ��Name$ 
tbFileName��5      $��DemiBold"�5      $��25��5      $	��300��5       $��3q�5!      $��Center=�5"      5#      5$      �� 5&   
   $��5��6   $��2��6"   $ 
Horizontal=�66   $��Right=�6O   $��10q�6k   .+�5'      �� -   5         ��Name$ btnCreateFile��5'      $��Create File��6,   $��5q�5(      $��Right=�6!   $��Bottom=�5)      $��DemiBold"�5*      $��10, 0q�5+      6$   �� 5,      -   5        $ 	btnCancel��5,      $��Cancel��6(   $��5q�5-      $��Right=�6!   $��Bottom=�5.      $��DemiBold"�5/      6+   50      51      52      