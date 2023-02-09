#ifndef INFO_H
#define INFO_H

#define HELP_S \
"Usage: mushcc [options] file...\n" \
"Options:\n" \
"  -h | --help       Display this helpful text :)\n" \
"  -v | --version    Display compiler information.\n" \
"  -o <file>         Place the output into <file>.\n" \
"  -c                Compile to C source file.\n" \
"  -s                Compile to assembly source file.\n" \
"\n" \
"For bug report, please contact\n" \
"<admin@mushtle.co.kr>.\n"
#define VERSION_S \
"mushcc 1.0\n" \
"Copyright (C) 2023 yj0524_kr\n" \
"\n" \
"mushroomlang\n" \
"Copyright (C) 2023 yj0524_kr\n"

#define SRC 0x1
#define ASM 0x2

#endif
