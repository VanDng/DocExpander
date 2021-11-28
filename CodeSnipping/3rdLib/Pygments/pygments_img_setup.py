from distutils.core import setup
from glob import glob
import py2exe

# Reference https://stackoverflow.com/questions/35948646/missing-modules-in-py2exe

data_files = [("Microsoft.VC90.CRT", glob(r'C:\Program Files\Microsoft Visual Studio 9.0\VC\redist\x86\Microsoft.VC90.CRT\*.*'))]
setup(
    name='ApplicationName',
    console=['pygments_img.py'], # 'windows' means it's a GUI, 'console' It's a console program, 'service' a Windows' service, 'com_server' is for a COM server
    # You can add more and py2exe will compile them separately.
    options={ # This is the list of options each module has, for example py2exe, but for example, PyQt or django could also contain specific options
        'py2exe': {
            'packages': [],
            'dist_dir': 'dist', # The output folder
            'compressed': True, # If you want the program to be compressed to be as small as possible
            'includes': [
                            'os',
                            'sys',
                            'getopt',
                            'pygments.',
                            'pygments.lexers', 
                            'pygments.formatters',
                            'pygments.formatters.img'
                        ], # All the modules you need to be included, I added packages such as PySide and psutil but also custom ones like modules and utils inside it because py2exe guesses which modules are being used by the file we want to compile, but not the imports, so if you import something inside main.py which also imports something, it might break.
        }
    },

    data_files=data_files # Finally, pass the
)
