import os
import sys, getopt
from pygments import highlight
from pygments.lexers import get_lexer_for_filename
from pygments.formatters import ImageFormatter

def main(argv):
    formatter_param = {
        "code_file_path": "",
        "line_pad": 5,
        "line_number_start": 1,
        "font_size": 15,
        "hl_lines": [],
        "hl_color": "#ffea00"}

    supported_args = list(formatter_param.keys())

    try:
        opts_array = [(arg + "=") for arg in supported_args]
        opts_array.append('help')

        opts, args = getopt.getopt(argv, "", opts_array)
    except getopt.GetoptError as err:
        print('Something went wrong.' + err)
        sys.exit(2)

    for opt, arg in opts:
        trunc_opt = opt[2:]

        if (trunc_opt == 'help'):
            print("Available arguments: " + str(supported_args))
            sys.exit(0)

        if trunc_opt in supported_args:
            if (trunc_opt == "hl_lines"):
                print(arg)
                formatter_param[trunc_opt] = arg.split(',')
            else:
                formatter_param[trunc_opt] = arg

    code_file = formatter_param["code_file_path"]
    code = read_code(code_file)

    code_file_name = os.path.basename(code_file)
    lexer = get_lexer_for_filename(code_file_name)
    formatter = ImageFormatter(line_pad          = formatter_param["line_pad"],
                               line_number_start = formatter_param["line_number_start"],
                               font_size         = formatter_param["font_size"],
                               hl_lines          = formatter_param["hl_lines"],
                               hl_color          = formatter_param["hl_color"])

    result = highlight(code, lexer, formatter)

    output_file = os.path.dirname(code_file) + '\\' + os.path.basename(code_file).split(".")[0] + ".png"
    write_file(output_file, result)

def read_code(filePath):
    file = open(filePath, mode='r')
    all_of_it = file.read()
    file.close()
    return all_of_it

def write_file(filePath, content):
    file = open(filePath, mode='bw')
    file.write(content)
    file.close()

if __name__ == "__main__":
   main(sys.argv[1:])