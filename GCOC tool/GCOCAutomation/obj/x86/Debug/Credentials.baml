   M S B A M L   `   `   `  ����  GCOCAutomation     GCOCAutomation.Credentials   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  Credentials��5      $	��361��6   $	��500��6&    � WindowStyle$ 
ToolWindow=�62    � 
ResizeMode$ NoResize=�6K    � WindowStartupLocation$ CenterScreen=�6a    � Topmost .   � ShowInTaskbar .  ���	#FFD5F9FF.��5   
   � $	��323��6   ���	#FFD5F9FF � RowDefinitions 5   
   �� 5      $	��277 �6   6+   �� 5	      $��28 �6   6+   .+�5   
   �� -   5        $��TabControl1��5      $��0,0,0,6q�6(   ���	#FFD5F9FF���	#FF5DE7FF � 	Resources 5       �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 5      " ��6   .�5      �� 	 � ��
 5      �� 5      " ��6.   .R�5   "   �� $��Border��6)   $��1,1,1,0q�67   ���	#FF5DE7FF$ 4,4,0,0��6i   ._�5   &   �� $ ContentSite��67   $��Center=�5   )   $��Center=�5   )   $��Header��5   )   $
��10,5q�5   )   5   #    5   "   P� 5   &    �@��
IsSelected . . �5   *   �� $ Border��61   	 � ��
 �	#FF5DE7FF6[   �� 5   *   	 � ��$
 0q�6C   5   '   P� 5   &    �@��
IsSelected .  . �5   *   �� $ Border��61   	 � ��
 �	#FFD5F9FF6[   �� 5!   *   	 � ��$

 -1,0q�6C   5"   '   5$      5&      5'      5(      .@�5)      �� -   5        $��tabIn��5)   %   $��India��6   ���	#FFD5F9FF.��5*      � $	��238��6    5+      �� 5,      $��50 �6(   �� 5-      $��50 �6(   �� 5.      $��50 �6(   �� 5/      $��90 �6(    � ColumnDefinitions 52      �� 53      $	��110 �6+   �� 54      $	��350 �6+   �� 55      $��7* �6+   .+�57      �� $��Company     :��6   $
��Left=�64   $	��top=�6P   $��6,6,0,6q�6h   $	��123��6y   $��25��6�   $��1��6�   �� 58      $��User ID         :��6   $��25��68   $
��Left=�6D   $	��top=�6_   $��6,6,0,6q�6w   $	��123��6�   $��2��6�   �� 59      $��Password     :��6   $��25��65   $
��Left=�6A   $	��top=�6\   $��6,6,0,19q�6t   $	��123��6�   $��3��6�   �� 5:      -   5        $��tbINCompany��5:   b   $��1��6   $��1��6.   $��25��6;   $
��Left=�6G   $	��top=�6u   $	��300��6�   $��6,6,0,0q�6�   �� 5;      -   5        $��tbINID��5;   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��top=�6d   $	��300��6|   $��1��6�   $��2��6�   G� 5<      -   5        $��tbINPW��5<   Z   $��25��6"   $
��Left=�6.   $��6,6,0,0q�6I   $	��top=�6h   $	��300��6�   $��1��6�   $��3��6�   5>      5?      �� 5@      -   5        $��tabSL��5@   )   $��	Sri Lanka��6   ���	#FFD5F9FF.��5A      �  5B      �� 5C      $��50 �6(   �� 5D      $��50 �6(   �� 5E      $��50 �6(   �� 5F      $��90 �6(    5I      �� 5J      $	��110 �6+   �� 5K      $	��350 �6+   �� 5L      $��7* �6+   .+�5N      �� -   5        $��lblSLCompany��5N   x   $��Company     :��6   $
��Left=�64   $	��top=�6O   $��6,6,0,6q�6g   $	��123��6�   $��25��6�   $��1��6�   �� 5O      -   5        $��lblSLID��5O   �   $��User ID         :��6   $��25��68   $
��Left=�6D   $	��top=�6_   $��6,6,0,6q�6w   $	��123��6�   $��2��6�   �� 5P      -	   5        $��lblSLPW��5P   �   $��Password     :��6   $��25��65   $
��Left=�6A   $	��top=�6\   $��6,6,0,20q�6t   $	��123��6�   $��3��6�   �� 5Q      -
   5        $��tbSLCompany��5Q   b   $��1��6   $��1��6.   $��25��6;   $
��Left=�6G   $	��Top=�6u   $	��300��6�   $��6,6,0,0q�6�   �� 5R      -   5        $��tbSLID��5R   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6d   $	��300��6|   $��1��6�   $��2��6�   G� 5S      -   5        $��tbSLPW��5S   Z   $��25��6"   $
��Left=�6.   $��6,6,0,0q�6I   $	��Top=�6h   $	��300��6�   $��1��6�   $��3��6�   5U      5V      �� 5W      -   5        $��tabPlant��5W   *   $��
Plant Data��6   ���	#FFD5F9FF.��5X      �  5Y      �� 5Z      $��15 �6(   �� 5[      $��35 �6(   �� 5\      $��35 �6(   �� 5]      $��35 �6(   �� 5^      $��35 �6(   �� 5_      $��35 �6(   �� 5`      $��50 �6(    5c      �� 5d      $	��150 �6+   �� 5e      $	��310 �6+   �� 5f      $��7* �6+   .+�5h      �� $��Actual Factory Location :��6   $
��Left=�6@   $	��Top=�6[   $��6,6,0,6q�6s   $	��150��6�   $��25��6�   $��1��6�   �� 5i      $��Name of Company        :��6   $��25��6?   $
��Left=�6K   $	��Top=�6f   $��6,6,0,6q�6~   $	��150��6�   $��2��6�   �� 5j      $&�� Full Address                   :��6   $��25��6G   $
��Left=�6S   $	��Top=�6n   $��6,6,0,6q�6�   $	��150��6�   $��3��6�   �� 5k      $$��Email Address                :��6   $��25��6E   $
��Left=�6Q   $	��Top=�6l   $��6,6,0,6q�6�   $	��150��6�   $��4��6�   �� 5l      $��Telephone number        :��6   $��25��6@   $
��Left=�6L   $	��Top=�6g   $��6,6,0,6q�6   $	��150��6�   $��5��6�   �� 5m      $#��Name of Certifier           :��6   $��25��6D   $
��Left=�6P   $	��Top=�6k   $��6,6,0,6q�6�   $	��150��6�   $��6��6�   �� 5o      -   5        $��tbPlantLocation��5o   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6m   $	��300��6�   $��1��6�   $��1��6�    �� 	MaxLength$ 75��6�   �� 5p      -   5        $��tbPlantName��5p   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6i   $	��300��6�   $��1��6�   $��2��6�   $ 80��6�   �� 5q      -   5        $��
tbPlantAdd��5q   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6h   $	��300��6�   $��1��6�   $��3��6�   $ 73��6�   �� 5r      -   5        $��tbPlantEmail��5r   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6j   $	��300��6�   $��1��6�   $��4��6�   $ 50��6�   �� 5s      -   5        $��	tbPlantTp��5s   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6g   $	��300��6   $��1��6�   $��5��6�   $ 30��6�   �� 5t      -   5        $��tbPlantCertifiName��5t   V   $��25��6   $
��Left=�6*   $��6,6,0,0q�6E   $	��Top=�6p   $	��300��6�   $��1��6�   $��6��6�   $ 50��6�   5w      5x      5y      �� 5|   
   -   5        $��btnSave��5|   g   $
��Save��6   $��2��6    $��23��6-   $��0,5,10,0q�69   $��Right=�6K   $	��Top=�6v   $	��100��6�   5}      5~      