   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DumbScrum	DumbScrum  5   	       DumbScrum.AddStoryWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DumbScrum     5   	     � Title$  	Add Story��5   	   $	��300��6   $	��500��6(    � WindowStartupLocation$ CenterScreen=�64    � WindowStyle$ 
ToolWindow=�6Y   .��5	      �  � RowDefinitions 5
   
   �� 5      $��15 �6   �� 5      $��50 �6   �� 5      �� 5      �� 5      �� 5      �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ txtInstructions��5      $��1��5      $��2��6!   $
��Wrap=�65    �� Padding$ 50, 10q�5       �� TextAlignment$ Center=�6%   $��14(�6<   $��SemiBold"�6J   SQPlease enter the following information about the user story that you want to add.5      �� 5   
   $��2��6   $��0��6   $��As a:��5      $��DemiBold"�5      $��10, 0q�5      $��Center=�5       $��Right=�5!      �� 5"   
   $��3��6   $��0��6   $��I would like to:��5#      $��DemiBold"�5$      $��10, 0q�5%      $��Center=�5&      $��Right=�5'      �� 5(   
   $��4��6   $��0��6   $��So that:��5)      $��DemiBold"�5*      $��10, 0q�5+      $��Center=�5,      $��Right=�5-      �� 5.   
   -   5         ��Name$ 	txtPerson��5.      $��2��6%   $��1��62   $��DemiBold"�5/      $��10, 3, 40, 3q�50      $��10, 0q�6&   $��Center=�51      $
��Left=�52      �� 53   
   -   5        $ 	txtAction��53      $��3��6%   $��1��62   $��DemiBold"�54      $��10, 3, 40, 3q�55      $��10, 0q�6&   $��Center=�56      $
��Left=�57      �� 58   
   -   5        $ 	txtReason��58      $��4��6%   $��1��62   $��DemiBold"�59      $��10, 3, 40, 3q�5:      $��10, 0q�6&   $��Center=�5;      $
��Left=�5<      �� 5=   
   -   5        	 ��Name$	 btnAddStory��5=      $��	Add Story��6&   $��5��5>      $��0��6   $	��125��5?      $��Right=�6   $��DemiBold"�5@      $��10, 3q�5A      6    �� 5B   
   -   5        $	 	btnCancel��5B      $��Cancel��6$   $��5��5C      $��1��6   $	��125��5D      $
��Left=�6   $��DemiBold"�5E      $��10, 3q�5F      6    5G      5H      