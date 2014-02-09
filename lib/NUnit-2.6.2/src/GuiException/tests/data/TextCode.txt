01	#include <stdio.h>
02	#include <stdlib.h>
03	#include <string.h>
04	#include <ctype.h>
05	#include <time.h>
06	#include <sys/types.h>
07	#include <sys/stat.h>
08
09	/* logic */
10	#ifndef TRUE
11	# define TRUE 1
12	#endif /* TRUE */
13	#ifndef FALSE
14	# define FALSE 0
15	#endif /* FALSE */
16	#define EOF_OK TRUE
17	#define EOF_NOT_OK FALSE
18
19	/* global limits */
20	#define RULE_YEAR 2004		/* NOTE: should match the current year */
21	#define START_DATE "07Jan2004 00:00 UTC" /* first confirmation received */
22	#define MAX_COL 79		/* max column a line should hit */
23	#define MAX_BUILD_SIZE 521	/* max how to build size */
24	#define MAX_PROGRAM_SIZE 4096	/* max program source size */
25	#define MAX_PROGRAM_SIZE2 2048	/* max program source size not counting
26					   whitespace and {}; not followed by
27					   whitespace or EOF */
28	#define MAX_TITLE_LEN 31	/* max chars in the title */
29	#define MAX_ENTRY_LEN 1		/* max length in the entry input line */
30	#define MAX_ENTRY 8		/* max number of entries per person per year */
31	#define MAX_FILE_LEN 1024	/* max filename length for a info file */
32
33	/* where to send entries */
34	#define ENTRY_USER "e.2004"
35	#define ENTRY_HOST "ioccc.org"
36
37	/* uuencode process - assumes ASCII */
38	#define UUENCODE(c) ((c) ? encode_str[(int)(c)&0x3f] : '`')
39	#define UUENCODE_LEN 45		/* max uuencode chunk size */
40	#define UUINFO_MODE 0444	/* mode of an info file's uuencode file */
41	#define UUBUILD_MODE 0444	/* mode of the build file's uuencode file */
42	#define UUBUILD_NAME "build"	/* name for the build file's uuencode file */
43	#define UUPROG_MODE 0444	/* mode of the program's uuencode file */
44	#define UUPROG_NAME "prog.c"	/* name for the program's uuencode file */
45
46	/* encode_str[(char)val] is the uuencoded character of val */
47	char encode_str[] = "`!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_";
48
49	/* global declarations */
50	char *program;			/* our name */
51	long start_time;		/* the startup time */
52
53	/* forward declarations */
54	void parse_args(int argc, char **argv, char **rname,
55				char **bname, char **pname, char **oname);
56	void usage(int exitval);
57	FILE *open_remark(char *filename);
58	FILE *open_build(char *filename);
59	FILE *open_program(char *filename);