   M S B A M L   `   `   `  ����   GCOCAutomation     GCOCAutomation.Credentials   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Credentials��$	��361��$	��500�� � WindowStyle$ 
ToolWindow=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � Topmost .   � ShowInTaskbar .  ���	#FFD5F9FF.��� $	��323�����	#FFD5F9FF � RowDefinitions �� $	��277 ��� $��28 �.+��� -   $��TabControl1��$��0,0,0,6q����	#FFD5F9FF���	#FF5DE7FF � 	Resources  �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� " ��.��� 	 � ��
 �� " ��.R��� $��Border��$��1,1,1,0q����	#FF5DE7FF$ 4,4,0,0��._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,5q� P�  �@��
IsSelected . . ��� $ Border��	 � ��
 �	#FF5DE7FF�� 	 � ��$
 0q�P�  �@��
IsSelected .  . ��� $ Border��	 � ��
 �	#FFD5F9FF�� 	 � ��$

 -1,0q�.@��� -   $��tabIn��$��India�����	#FFD5F9FF.��� $	��238�� �� $��50 ��� $��50 ��� $��50 ��� $��90 � � ColumnDefinitions �� $	��110 ��� $	��350 ��� $��7* �.+��� $��Company     :��$
��Left=�$	��top=�$��6,6,0,6q�$	��123��$��25��$��1���� $��User ID         :��$��25��$
��Left=�$	��top=�$��6,6,0,6q�$	��123��$��2���� $��Password     :��$��25��$
��Left=�$	��top=�$��6,6,0,19q�$	��123��$��3���� -   $��tbINCompany��$��1��$��1��$��25��$
��Left=�$	��top=�$	��300��$��6,6,0,0q��� -   $��tbINID��$��25��$
��Left=�$��6,6,0,0q�$	��top=�$	��300��$��1��$��2��G� -   $��tbINPW��$��25��$
��Left=�$��6,6,0,0q�$	��top=�$	��300��$��1��$��3���� -   $��tabSL��$��	Sri Lanka�����	#FFD5F9FF.���  �� $��50 ��� $��50 ��� $��50 ��� $��90 � �� $	��110 ��� $	��350 ��� $��7* �.+��� -   $��lblSLCompany��$��Company     :��$
��Left=�$	��top=�$��6,6,0,6q�$	��123��$��25��$��1���� -   $��lblSLID��$��User ID         :��$��25��$
��Left=�$	��top=�$��6,6,0,6q�$	��123��$��2���� -	   $��lblSLPW��$��Password     :��$��25��$
��Left=�$	��top=�$��6,6,0,20q�$	��123��$��3���� -
   $��tbSLCompany��$��1��$��1��$��25��$
��Left=�$	��Top=�$	��300��$��6,6,0,0q��� -   $��tbSLID��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��2��G� -   $��tbSLPW��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��3���� -   $��tabPlant��$��
Plant Data�����	#FFD5F9FF.���  �� $��15 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��50 � �� $	��150 ��� $	��310 ��� $��7* �.+��� $��Actual Factory Location :��$
��Left=�$	��Top=�$��6,6,0,6q�$	��150��$��25��$��1���� $��Name of Company        :��$��25��$
��Left=�$	��Top=�$��6,6,0,6q�$	��150��$��2���� $&�� Full Address                   :��$��25��$
��Left=�$	��Top=�$��6,6,0,6q�$	��150��$��3���� $$��Email Address                :��$��25��$
��Left=�$	��Top=�$��6,6,0,6q�$	��150��$��4���� $��Telephone number        :��$��25��$
��Left=�$	��Top=�$��6,6,0,6q�$	��150��$��5���� $#��Name of Certifier           :��$��25��$
��Left=�$	��Top=�$��6,6,0,6q�$	��150��$��6���� -   $��tbPlantLocation��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��1�� �� 	MaxLength$ 75���� -   $��tbPlantName��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��2��$ 80���� -   $��
tbPlantAdd��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��3��$ 73���� -   $��tbPlantEmail��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��4��$ 50���� -   $��	tbPlantTp��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��5��$ 30���� -   $��tbPlantCertifiName��$��25��$
��Left=�$��6,6,0,0q�$	��Top=�$	��300��$��1��$��6��$ 50���� -   $��btnSave��$
��Save��$��2��$��23��$��0,5,10,0q�$��Right=�$	��Top=�$	��100��