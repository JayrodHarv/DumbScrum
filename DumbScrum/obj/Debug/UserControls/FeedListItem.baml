   M S B A M L   `   `   `  ����D  @DumbScrum, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:DumbScrum.UserControlsDumbScrum.UserControls  5      )    #DumbScrum.UserControls.FeedListItem   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:DumbScrum.UserControls     5      .��5	      � 
�������$��0,20,0,0q�6     � RowDefinitions  5
   
   �� 5      $��50 �6   �� 5      �� 5      $��30 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��
10,10,10,0q�6;   .+�5      A� $��35��6   $��35��6"   ��5      �� 5       �� ImageSource 6!   �� *	UserPfp+5      �� 5      $ Vertical=�6   .+�5      �� -   5         ��Name$ 	txtSentBy��5      $	��5,0q�6P   $��16(�6]   ��6/   �� *UserDisplayName+6]   �� 5      -   5        $ 	txtSentAt��5      $	��5,0q�6G   $��10(�6T   ��6/   �� *SentAt+6T   5      5      �� 5   
   -   5        $ 
lblMessage��5   !   $��1��6   $��12(�6K   $
��Wrap=�6Y   $
��10,0q�6m   ��65   �� *Text+6m   5      5      