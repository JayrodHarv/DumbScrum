   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DumbScrum	DumbScrum  5   	   $    DumbScrum.ChangePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DumbScrum     5   	     � Title$  Change Password��5   	   $	��300��6!   $	��500��6.    � WindowStartupLocation$ CenterScreen=�6:    � WindowStyle$ 
ToolWindow=�5	   	    � 
ResizeMode$ NoResize=�6"   
���   � � Topmost . .��5
      �  � RowDefinitions 5   
   �� 5      $��15 �6   �� 5      $��50 �6   �� 5      �� 5      �� 5      �� 5      �� 5      $��25 �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ txtInstructions��5      $��1��5      $��2��6!   $
��Wrap=�65    �� Padding$ 50, 10q�5      	 �� TextAlignment$	 Center=�6%   $��14(�6<   $��SemiBold"�6J   trTo change your password, you need to enter the email belonging to the account who's password you'd like to change.5      �� 5   
   -   5        
 ��Name$
 lblEmail��5      $��2��6"   $��0��6/   $��Email:��5      $��DemiBold"�5      $��10, 0q�5       $��Center=�5!      $��Right=�5"      �� 5#   
   -   5        $
 lblNewPassword��5#      $��3��6(   $��0��65   $��New Password:��5$      $��DemiBold"�5%      $��10, 0q�5&      $��Center=�5'      $��Right=�5(      �� 5)   
   -   5        $
 lblRetypePassword��5)      $��4��6+   $��0��68   $��Retype Password:��5*      $��DemiBold"�5+      $��10, 0q�5,      $��Center=�5-      $��Right=�5.      �� 5/   
   -   5         ��Name$ txtEmail��5/      $��2��6$   $��1��61   $��DemiBold"�50      $��10, 3, 40, 3q�51      $��10, 0q�6&   $��Center=�52      $
��Left=�53      G� 54   
   -   5         G�Name$ pwdNewPassword��54      $��3��6.   $��1��6;   $��DemiBold"�55      $��10, 3, 40, 3q�56      $��10, 0q�6&   $��Center=�57      $
��Left=�58      G� 59   
   -   5        $ pwdRetypePassword��59      $��4��61   $��1��6>   $��DemiBold"�5:      $��10, 3, 40, 3q�5;      $��10, 0q�6&   $��Center=�5<      $
��Left=�5=      �� 5>   
   -   5         ��Name$ 	btnSubmit��5>      $��Submit��6$   $��5��5?      $��0��6   $	��125��5@      $��Right=�6   $��DemiBold"�5A      $��10, 3q�5B      6    �� 5C   
   -	   5        $ 	btnCancel��5C      $��Cancel��6$   $��5��5D      $��1��6   $	��125��5E      $
��Left=�6   $��DemiBold"�5F      $��10, 3q�5G      6    5H      5I      