   M S B A M L   `   `   `  ����T  PCoffeeMachineSimulator.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:FireDeviceSimulator.UIFireDeviceSimulator.UI  5   	   '    !FireDeviceSimulator.UI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:FireDeviceSimulator.UI     5   	     � Title$  Alarm Event Simulator��5   	   $	��450��6'   $	��525��64   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��2��6    �� Orientation$ 
Horizontal=�6)   
�������.+�5      �� $��Images\Meeting.ico��6   $	��120��60   $��80��6<   �� 5      $��Fire Device Simulator��6   $��30(�5      $��Center=�6&   $
��Bold"�5      ���#3333336*   5      ]� 5   
   $��1��6   .+�5      �� $��City : ��6   $��5 5 0 5q�6'   �� 5      $��5q�5   Y   ��6   �� *City+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6Y   5       ]� 5!   
   $��1��6   $��1��6!   .+�5"      �� $��Serial Number : ��6   $��5 5 0 5q�60   �� 5#      $��5q�6f   ��6   �� *SerialNumber+$ TwoWay=�$ PropertyChanged=�6f   5$      �� 5&   
   $��Send on Action��6   $��2��6+   $��5q�68   .��5'      �� .+�5(      �� $��5q�6   ��6$   �� *TriggerAlarmCommand+.��5)      �� ��6    �� *AlarmCounter+ �� StringFormat$ Trigger Alarm ({0})�� �� FallbackValue$ Trigger Alarm��5+      �� 5,      $��5q�6A   ��6   �� *TriggerAlertCommand+.��5-      �� ��6    �� *AlertCounter+$ Trigger Alert ({0})��$ Trigger Alert��5/      50      51      �� 53   
   $��Send Periodically��6   $��2��6.   $��1��6;   $��5q�6K   .��54      �� .+�55      �� $��
IS Sending��6    d� 	IsChecked 60   �� *IsSendingPeriodically+$ TwoWay=�$ PropertyChanged=��� 56      ��6   �� *BoilerTemperature+$ Boiler temp: {0} deg C���� 57      	 � Maximum$		 100Q�6J   
 � Value
 6   �� *BoilerTemperature+$ TwoWay=�6J   �� 58      ��6   �� *	BeanLevel+$ Bean Level : {0} %���� 59      $		 100Q�6B   
 6   �� *	BeanLevel+$ TwoWay=�6B   5:      5;      �� 5=   
   $��3��6   $��2��6$   $��5��68   $��Stretch=�6C   �� 5>   
   $
��Logs��6   $��4��6!   $��2��6.   .��5?      �� ��6   �� *Logs+5@      5A      5B      