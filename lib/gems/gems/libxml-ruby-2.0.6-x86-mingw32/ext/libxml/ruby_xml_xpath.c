/*
 * Document-class: LibXML::XML::XPath
 *
 * The XML::XPath module is used to query XML documents. It is
 * usually accessed via the XML::Document#find or
 * XML::Node#find methods.  For example:
 *
 *  document.find('/foo', namespaces) -> XML::XPath::Object
 *
 * The optional namespaces parameter can be a string, array or
 * hash table.
 *
 *   document.find('/foo', 'xlink:http://www.w3.org/1999/xlink')
 *   document.find('/foo', ['xlink:http://www.w3.org/1999/xlink',
 *                          'xi:http://www.w3.org/2001/XInclude')
 *   document.find('/foo', 'xlink' => 'http://www.w3.org/1999/xlink',
 *                             'xi' => 'http://www.w3.org/2001/XInclude')
 *
 *
 * === Working With Default Namespaces
 *
 * Finding namespaced elements and attributes can be tricky.
 * Lets work through an example of a document with a default
 * namespace:
 *
 *  <?xml version="1.0" encoding="utf-8"?>
 *  <feed xmlns="http://www.w3.org/2005/Atom">
 *    <title type="text">Phil Bogle's Contacts</title>
 *  </feed>
 *
 * To find nodes you must define the atom namespace for
 * libxml.  One way to do this is:
 *
 *   node = doc.find('atom:title', 'atom:http://www.w3.org/2005/Atom')
 *
 * Alternatively, you can register the default namespace like this:
 *
 *   doc.root.namespaces.default_prefix = 'atom'
 *   node = doc.find('atom:title')
 *
 * === More Complex Namespace Examples
 *
 * Lets work through some more complex examples using the
 * following xml document:
 *
 *  <soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
 *    <soap:Body>
 *      <getManufacturerNamesResponse xmlns="http://services.somewhere.com">
 *        <IDAndNameList xmlns="http://services.somewhere.com">
 *          <ns1:IdAndName xmlns:ns1="http://domain.somewhere.com"/>
 *        </IDAndNameList>
 *      </getManufacturerNamesResponse>
 *  </soap:Envelope>
 *
 *  # Since the soap namespace is defined on the root
 *  # node we can directly use it.
 *  doc.find('/soap:Envelope')
 *
 *  # Since the ns1 namespace is not defined on the root node
 *  # we have to first register it with the xpath engine.
 *  doc.find('//ns1:IdAndName',
 *           'ns1:http://domain.somewhere.com')
 *
 *  # Since the getManufacturerNamesResponse element uses a default
 *  # namespace we first have to give it a prefix and register
 *  # it with the xpath engine.
 *  doc.find('//ns:getManufacturerNamesResponse',
 *            'ns:http://services.somewhere.com')
 *
 *  # Here is an example showing a complex namespace aware
 *  # xpath expression.
 *  doc.find('/soap:Envelope/soap:Body/ns0:getManufacturerNamesResponse/ns0:IDAndNameList/ns1:IdAndName',
 *  ['ns0:http://services.somewhere.com', 'ns1:http://domain.somewhere.com'])
*/


#include "ruby_libxml.h"

VALUE mXPath;

void rxml_init_xpath(void)
{
  mXPath = rb_define_module_under(mXML, "XPath");

  /* 0: Undefined value. */
  rb_define_const(mXPath, "UNDEFINED", INT2NUM(XPATH_UNDEFINED));
  /* 1: A nodeset, will be wrapped by XPath Object. */
  rb_define_const(mXPath, "NODESET", INT2NUM(XPATH_NODESET));
  /* 2: A boolean value. */
  rb_define_const(mXPath, "BOOLEAN", INT2NUM(XPATH_BOOLEAN));
  /* 3: A numeric value. */
  rb_define_const(mXPath, "NUMBER", INT2NUM(XPATH_NUMBER));
  /* 4: A string value. */
  rb_define_const(mXPath, "STRING", INT2NUM(XPATH_STRING));
  /* 5: An xpointer point */
  rb_define_const(mXPath, "POINT", INT2NUM(XPATH_POINT));
  /* 6: An xpointer range */
  rb_define_const(mXPath, "RANGE", INT2NUM(XPATH_RANGE));
  /* 7: An xpointer location set */
  rb_define_const(mXPath, "LOCATIONSET", INT2NUM(XPATH_LOCATIONSET));
  /* 8: XPath user type */
  rb_define_const(mXPath, "USERS", INT2NUM(XPATH_USERS));
  /* 9: An XSLT value tree, non modifiable */
  rb_define_const(mXPath, "XSLT_TREE", INT2NUM(XPATH_XSLT_TREE));
}
