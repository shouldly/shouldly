/* Please see the LICENSE file for copyright and distribution information */

#ifndef __RUBY_LIBXML_H__
#define __RUBY_LIBXML_H__

#include <ruby.h>
#include <libxml/parser.h>
#include <libxml/parserInternals.h>
#include <libxml/debugXML.h>
#include <libxml/xmlversion.h>
#include <libxml/xmlmemory.h>
#include <libxml/xpath.h>
#include <libxml/valid.h>
#include <libxml/catalog.h>
#include <libxml/HTMLparser.h>
#include <libxml/xmlreader.h>
#include <libxml/c14n.h>

/* Needed for Ruby 1.8.5 */
#ifndef RARRAY_LEN
#define RARRAY_LEN(s) (RARRAY(s)->len)
#endif

/* Needed for Ruby 1.8.5 */
#ifndef RARRAY_PTR
#define RARRAY_PTR(s) (RARRAY(s)->ptr)
#endif

/* Needed for Ruby 1.8.5 */
#ifndef RSTRING_LEN
#define RSTRING_LEN(s) (RSTRING(s)->len)
#endif

/* Needed for Ruby 1.8.5 */
#ifndef RSTRING_PTR
#define RSTRING_PTR(s) (RSTRING(s)->ptr)
#endif

/* Needed prior to Ruby 1.9.1 */
#ifndef RHASH_TBL
#define RHASH_TBL(s) (RHASH(s)->tbl)
#endif

// Not in Ruby 1.9
#ifndef GetWriteFile
#define GetWriteFile(fp) rb_io_stdio_file(fp)
#define OpenFile rb_io_t
#endif

// Encoding support added in Ruby 1.9.*
#ifdef HAVE_RUBY_ENCODING_H
#include <ruby/encoding.h>
#endif

// Ruby 1.8/1.9 encoding compatibility
VALUE rxml_str_new2(const char* xstr, const char* xencoding);

#ifdef LIBXML_DEBUG_ENABLED
#include <libxml/xpathInternals.h>
#endif
#ifdef LIBXML_XINCLUDE_ENABLED
#include <libxml/xinclude.h>
#endif
#ifdef LIBXML_XPTR_ENABLED
#include <libxml/xpointer.h>
#endif

#include "ruby_xml_version.h"
#include "ruby_xml.h"
#include "ruby_xml_io.h"
#include "ruby_xml_error.h"
#include "ruby_xml_encoding.h"
#include "ruby_xml_attributes.h"
#include "ruby_xml_attr.h"
#include "ruby_xml_attr_decl.h"
#include "ruby_xml_document.h"
#include "ruby_xml_node.h"
#include "ruby_xml_namespace.h"
#include "ruby_xml_namespaces.h"
#include "ruby_xml_parser.h"
#include "ruby_xml_parser_options.h"
#include "ruby_xml_parser_context.h"
#include "ruby_xml_html_parser.h"
#include "ruby_xml_html_parser_options.h"
#include "ruby_xml_html_parser_context.h"
#include "ruby_xml_reader.h"
#include "ruby_xml_sax2_handler.h"
#include "ruby_xml_sax_parser.h"
#include "ruby_xml_xinclude.h"
#include "ruby_xml_xpath.h"
#include "ruby_xml_xpath_expression.h"
#include "ruby_xml_xpath_context.h"
#include "ruby_xml_xpath_object.h"
#include "ruby_xml_xpointer.h"
#include "ruby_xml_input_cbg.h"
#include "ruby_xml_dtd.h"
#include "ruby_xml_schema.h"
#include "ruby_xml_relaxng.h"

extern VALUE mLibXML;

#endif
