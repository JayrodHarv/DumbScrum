   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DumbScrum	DumbScrum  5   	        DumbScrum.AddFeatureWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DumbScrum     5   	     � Title$  Add Feature��5   	   $	��300��6   $	��500��6*    � WindowStyle$ 
ToolWindow=�66    � WindowStartupLocation$ CenterScreen=�6O   .��5	      �  � RowDefinitions 5
   
   �� 5      $��15 �6   �� 5      $��50 �6   �� 5      �� 5      �� 5      �� 5      �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ txtInstructions��5      $��1��5      $��2��6!   $
��Wrap=�65    �� Padding$ 50, 10q�5       �� TextAlignment$ Center=�6%   $��14(�6<   $��SemiBold"�6J   PNPlease enter the following information about the feature that you want to add.5      �� 5   
   -   5         ��Name$ lblFeatureName��5      $��2��6(   $��0��65   $��Feature Name:��5      $��DemiBold"�5      $��10, 0q�5      $��Center=�5       $��Right=�5!      �� 5"   
   -   5        $ lblDescription��5"      $��3��6(   $��0��65   $��Description:��5#      $��DemiBold"�5$      $��10, 0q�5%      $��Center=�5&      $��Right=�5'      �� 5(   
   -   5        $ lblPriority��5(      $��4��6%   $��0��62   $��	Priority:��5)      $��DemiBold"�5*      $��10, 0q�5+      $��Center=�5,      $��Right=�5-      �� 5.   
   -   5        	 ��Name$	 txtFeatureName��5.      $��2��6*   $��1��67   $��DemiBold"�5/      $��10, 3, 40, 3q�50      $��10, 0q�6&   $��Center=�51      $
��Left=�52      �� 53   
   -   5        $	 txtDescription��53      $��3��6*   $��1��67   $��DemiBold"�54      $��10, 3, 40, 3q�55      $��10, 0q�6&   $��Center=�56      $
��Left=�57      �� 58   
   -   5        
 ��Name$
 cboPriority��58      $��4��6(   $��1��65   $��DemiBold"�59      $��10, 3, 40, 3q�5:      $��10, 0q�6&   $��Center=�5;      $
��Left=�5<      .@�5=      �� $	��Low��6   �� 5>      $��Medium��6   �� 5?      $
��High��6   5@      �� 5A   
   -   5         ��Name$ btnAddFeature��5A      $��Add Feature��6(   $��5��5B      $��0��6   $	��125��5C      $��Right=�6   $��DemiBold"�5D      $��10, 3q�5E      6    �� 5F   
   -	   5        $ 	btnCancel��5F      $��Cancel��6$   $��5��5G      $��1��6   $	��125��5H      $
��Left=�6   $��DemiBold"�5I      $��10, 3q�5J      6    5K      5L      