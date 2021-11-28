# Construction steps:

1/ Install Python

2/ Install Pygments
pip install Pygments

3/ Install Python Image Library
pip install Pillow

4/ Install Python2Exe
pip install py2exe

5/ Setup standalone executable. It ouputs a directory "dist".
python.exe .\pygments_img_setup.py py2exe

Note: Can run the batch "publish.bat" instead.

6/ Done

# Example
pygments_img.exe --code_file_path="test\TestDriver2.cs" --line_number_start=1 --line_pad=5 --font_size=15 --hl_lines="9 11 12" --hl_color="#829abd"
