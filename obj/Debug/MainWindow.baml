   M S B A M L   `   `   `  ����F  BBrush_Teeth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Brush_TeethBrush_Teeth  5   	       Brush_Teeth.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Brush_Teeth     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)   .��5	      � 
�������.+�5
   
   &   System.Windows.Controls.Calendar  -   5        $��BrushCalendar��5
      $��Right=�6(   $��Bottom=�6D   $��10q�6_   �� 5   
   -   5        $��Today��5      $��
A fine day��6!   $
��Left=�64   $	��Top=�6O   $��354,753,0,0q�6g   $��16(�6|   $	��232��6�   �� 5   
   -   5        $��BrushMornButton��5      $,��&Did you brush your teeth this morning?��6(   $��Right=�6Y   $	��Top=�6u   $��	0,10,12,0q�6�   $��16(�6�   $	��287��6�   �� 5   
   -   5        $��
WashButton��5      $"��Did you use mouthwash today?��6#   $��Right=�6J   $	��Top=�6f   $��	0,40,12,0q�6~   $��16(�6�   �� 5   
   -   5        $��BrushEveButton��5      $,��&Did you brush your teeth this evening?��6'   $��Right=�6X   $	��Top=�6t   $��	0,70,10,0q�6�   $��16(�6�   �� 5   
   -   5        $��FlossButton��5      $-��'Did you floss between your teeth today?��6$   $��Right=�6V   $	��Top=�6r   $��
0,100,12,0q�6�   $��16(�6�   �� 5   
   -   5        $��BrushStatus��5      $��Not Brushed��6'   $
��Left=�6:   $	��Top=�6U   $��
38,374,0,0q�6m   $��16(�6�   �� 5   
   -   5        $��BrushCanvas��5      .+�5      �� -	   5        $��
TeethImage��5      $��/Brush_Teeth/teeth.png��6&   $
��Left=�6F   $	��Top=�6a   $	��200��6y   $	��200��6�    J� Opacity$ 1Q�6�   $��Uniform=�6�   $y�Visible=�6�    �� Left$ 23��6�   
 �� Top$ 38��6�   �� 5      -
   5        $��Yellowed��5      $��/Brush_Teeth/yellowed.png��6$   $
��Left=�6G   $	��Top=�6b   $	��200��6z   $	��200��6�   $	 0.6Q�6�    K� ZIndex$ -1��6�   $��Uniform=�6�   $y�Visible=�6�   $ 23��6�   $ 38��6�   �� 5      -   5        $��
Toothbrush��5      $2��,pack://application:,,,/Brush_Teeth/brush.png��6&   $
��Left=�6\   $	��Top=�6w   $��50��6�   $��50��6�   $	 283��6�   $	 175��6�   $y�	Collapsed=�6�   5      �� 5   
   -   5        $��StreakStatus��5      $
��Nada��6(   $
��Left=�64   $	��Top=�6O   $��124,257,0,0q�6g   $��16(�6|   �� 5   
   -   5        $��LoginButton��5      $��Login��6$   $��Center=�64   $��Center=�6Q   $y�	Collapsed=�6�   �� 5   
   -   5        $��ImageButton��5      $��Upload Teeth!��6$   $
��Left=�6<   $	��Top=�6W    J� RenderTransformOrigin$ 1.073,0.601&�6�   $��675,337,0,0q�6�   $��16(�6�   �� 5   
   -   5        $��ImageDisplay��5      $
��Left=�6$   $	��Top=�6?   $	��200��6W   $	��200��6c   $��UniformToFill=�6p    �� StretchDirection$
 Both=�6�   $��562,130,0,0q�6�   5      5      