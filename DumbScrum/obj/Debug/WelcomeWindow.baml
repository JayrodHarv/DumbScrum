   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:DumbScrum	DumbScrum  5   	       DumbScrum.WelcomeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:DumbScrum     5   	   -   5          � Title$  Welcome To Dumb Scrum��5   	   $	��385��6'   $	��715��64    � WindowStartupLocation$ CenterScreen=�6@    � WindowStyle$ 
ToolWindow=�6|    � 
ResizeMode$ NoResize=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $	��350 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $	��700 �6   �� 5      .+�5   
   � $��1��6   $��1��6   
������� 5      �� 5      �� 5      $��35 �6    �� 5      $��25 �6    �� 5      $��30 �6    �� 5      $��10 �6    �� 5      $��25 �6    �� 5      $��30 �6    �� 5      $��30 �6    �� 5      $��30 �6    �� 5       $��30 �6    �� 5!      .+�5#      �� -   5         ��Name$ 
lblMessage��5#      $��1��6(   $#��Sign in to Dumb Scrum account��65   $
��Bold"�6]   $��Center=�5$      $��16(�68   6H   �� 5%      -   5        $ lblEmail��5%      $��2��6&   $��Email:��63   $��DemiBold"�6D   $��14(�6Z   $
��15,0q�6h   6y   �� 5&      -   5         ��Name$ txtEmail��5&      $��3��6(   $��20, 0q�65   $
��Left=�5'      $��Center=�68   .��6Z   5(      �� 5*      -   5        $ lblPassword��5*      $��5��6)   $��	Password:��66   $��DemiBold"�6J   $��14(�6`   $
��15,0q�6n   6~   G� 5+      -   5         G�Name$ pwdPassword��5+      $��6��6/   $��20, 0q�6<   $
��Left=�5,      $��Center=�68   5-      �� 5.      -   5        $ lblForgotPassword��5.      $��7��6/   $��Light"�6<   ���	#FF0139CC$
��15,0q�5/   +   .��50      �� -   5        .�50   <   Forgot your password?6S   51      �� 52      -	   5        	 ��Name$	 	btnSignIn��52      $��8��6(   $��Sign In��65   $��20, 0q�6G   $��14(�6V   6~   �� 53      -
   5        $ 
lblAccount��53      $��9��6(   $��Light"�65   ���	#FF0139CC$
��15,0q�54   +   $��Center=�69   .��55      �� -   5        
 ��Name$
 	hypSignIn��55      .�6G   .�56      �� -   5         ��Name$ hyperlinkText��56        Don't have an account? Sign Up58      6#   59      5:      5;      5<      