   M S B A M L   `   `   `  ����B  >LookArt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LookArtLookArt  5   	   )clr-namespace:LookArt.ResLookArt.Res  5   	       LookArt.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:LookArt     5   	   )Resclr-namespace:LookArt.Res     5   	     � Title$  LookArt v1.04��5	   	   $	��583��6   $
��1000��6,   ���#0f0f0f$	��550��5
   	   $
��1000��6    � Icon$ img/afuslogo.ico��5   	   .��5      � .+�5   
   �� -   5         ��Name$ nextImageButton��5      $��	0,0,10,10q�6:      
BaseButton#��[  6M   $��30(�6q      LookArt.Res.Strings   
NextButton#��Z 6   $��Right=�6�   $	��237��6�   $��67��6�   $��Bottom=�6�   �� 5   
   ���#1e1e1e$��0,0,500,162q�6*   .+�5      �� 
�������$��Bahnschrift)�6'   $��20(�6@      LabelFolder#��Z 6N   $��0,10,0,0q�6{   �� 5      -   5         ��Name$ folderTextBox��5      $��19,0,0,0q�6.   $	��323��6@   $��26��6L   $
��left=�6X   $��Center=�6s   6�   �� 5      
�������$��Bahnschrift)�6'   $��20(�6@      LabelTimerSeq#��Z 6N   $��0,10,0,0q�6}   ]� 5      .+�5      �� -   5        $ sequenceTextBox��5      $��19,0,0,0q�64   $	��323��6F   $��26��6R   $��Center=�6^   $��Center=�6{   6�   �� 5      -   5        $ startStopButton��5      $	��106��6L   #��[  6X      startButton#��Z 6|   5      �� 5      ���#c8c8c8$��Bahnschrift)�6*   $��16(�6C   $��	10,10,0,0q�6Q   .��5      �� $��WrapWithOverflow=�6      Example#��Z 6<   $	��217��6b   5      5      �� 5   
   -   5        	 ��Name$	 timeCounter��5      $��Bahnschrift)�6%   
�������$��80(�6Q   $��0,86,128,322q�6_   $��Right=�6u   $	��214��6�   .��6�   00:005      �� 5   
   $��	0,0,0,162q�6   ���#1e1e1e$��Right=�6=   $	��500��6Y   $��95��6e   $��Bottom=�6q   .+�5       �� $
��left=�6   
�������$��Bahnschrift)�6B   $��20(�6[   
   LabelPreset#��Z
 6i   $��15,0,0,0q�6�   $��5q�6�   ]� 5!      .+�5"      �� -   5        $ presetTextBox��5"      $��20,5,5,0q�62   $	��323��6D   $��26��6P   $
��left=�6\   $��Center=�6w   6�   �� 5#      -   5        $ startButtonPreset��5#      $��0,0,5,0q�64   $
��auto��6b   #��[  6o      presetButton#��Z 6�   5$      5%      �� 5&   
   -   5        $	 
errorLabel��5&      
���  ��$��Arial)�65   $��20(�6H   $��50,425,10,10q�6V   6n   ]� 5'   
   $��Bottom=�6   $
��Left=�6/   $	��366��6J   .+�5(      �� $��/img/afuslogo.ico��6   $
��Fill=�6/   $��58��6>   $��57��6I   $	��5,5q�6U   �� 5)      
�������$��20(�6'   $$��Made by Akto (Akt0o on github)��65   $��Bottom=�6^   5*      � 5+   
   -	   5         �Name$ 
imagePopUp��5+      $��false��6$   .��5,      �� $��1q�6   ._�5-      �� -
   5         ��Name$ CurrentImage��5-      5.      5/      50      51      52      