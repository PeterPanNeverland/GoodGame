   M S B A M L   `   `   `  ����D  @Game Main, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Game_Main	Game_Main  5   	       Game_Main.ScoreWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Game_Main     5   	     � Title$  ScoreWindow��5   	   $	��300��6   $	��300��6*   .��5	      � .+�5
   
   �� 
���   �$
��Left=�6$   $	��270��6?   $	��Top=�6L   $	��292��6d   .+�5      �� -   5         ��Name$ intro��5      $��Images\introbkg.jpg��6#   $	��181��6@    �� Left$ 34��6M   
 �� Top$ 22��6^   $	��216��6n   $
��Fill=�6z   �� 5      -   5        $ image��5      $	��100��6#   $ 64��60   $	 130��6A   $	��100��6R   &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgScoreList��5      
���   �
����ح�$ 64��6V   $	 130��6g   $	��130��6x   $	��164��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 5* 63      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6R   �� 
 �� Path$

 Name�  5      $ Score��6!   $ 5* 60    . 	 6N   �� $
 Score�5      5      5      5      