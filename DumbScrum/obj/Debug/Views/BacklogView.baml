   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:DumbScrum.ViewsDumbScrum.Views  5      ?$clr-namespace:DumbScrum.UserControlsDumbScrum.UserControls  5      !    DumbScrum.Views.BacklogView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:DumbScrum.Views     5      =userControls$clr-namespace:DumbScrum.UserControls     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��40 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $��5 �6   �� 5      .+�5   
   � $��0��6   $��0��6    5      �� 5      �� 5      .+�5      �� $��0��6   $��DemiBold"�6$   $��Project Features��6:   $��Center=�6U   $��18(�6q   �� 5      $��1��6    �� Orientation$ 
Horizontal=�6)   $��Right=�6B   $��0, 0, 10, 0q�6^   .+�5      �� -   5         ��Name$ btnAddFeature��5      $��Add Feature��60   $��DemiBold"�6F   $��5q�6\   
��� � �
�������$��Center=�6�   $��Center=�6�   6�   5      5      �� 5   
   $
��Auto=�6   $��1��6:   $��0��6G   .��5      �� -   5         ��Name$ 
lvFeatures��5      $��Stretch=�6L   ��6+   �� *
Features+��5       �� 5!      .R�5"      ,   &DumbScrum.UserControls.FeatureListItem  $��Stretch=�67   5#      5%      5&      �� 5'   
   $��2��6   $��1��6(    �� ResizeDirection$ Columns=�68   $��Stretch=�6R   
������ 6p   � 5(   
   $��0��6   $��2��6    5)      �� 5*      �� 5+      .+�5-      �� $��0��6   $��DemiBold"�6$   $��Feature Stories��6:   $��Center=�6T   $��18(�6p   �� 5.      $��1��6   $ 
Horizontal=�6)   $��Right=�6B   $��0, 0, 10, 0q�6^   .+�5/      �� -   5        $ btnAddStory��5/      $��	Add Story��6.   $��DemiBold"�6B   $��5q�6X   
��� � �
�������$��Center=�6�   $��Center=�6�   6�   50      51      �� 52   
   $
��Auto=�6   $��1��6:   $��2��6G   .��53      �� -   5        $ 	lvStories��53      ��54      �� 55      .R�56      .   (DumbScrum.UserControls.UserStoryListItem  $��Stretch=�69   57      59      5:      5;      5<      