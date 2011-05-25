/* Please see the LICENSE file for copyright and distribution information */

#ifndef __RXML_ENCODING__
#define __RXML_ENCODING__

extern VALUE mXMLEncoding;

void rxml_init_encoding();

#ifdef HAVE_RUBY_ENCODING_H
VALUE rxml_xml_encoding_to_rb_encoding(VALUE klass, xmlCharEncoding xmlEncoding);
#endif

#endif
