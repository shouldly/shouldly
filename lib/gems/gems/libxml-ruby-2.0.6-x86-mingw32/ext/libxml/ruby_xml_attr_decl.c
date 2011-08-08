/* Please see the LICENSE file for copyright and distribution information */

/*
 * Document-class: LibXML::XML::AttrDecl
 *
 * At attribute declaration is used in XML::Dtds to define 
 * what attributes are allowed on an element.  An attribute
 * declaration defines an attribues name, data type and default
 * value (if any).
 */

#include "ruby_libxml.h"

VALUE cXMLAttrDecl;

void rxml_attr_decl_mark(xmlAttributePtr xattr)
{
  if (xattr->_private == NULL)
  {
    rb_warning("AttrDecl is not bound! (%s:%d)", __FILE__, __LINE__);
    return;
  }

  rxml_node_mark((xmlNodePtr) xattr);
}

VALUE rxml_attr_decl_wrap(xmlAttributePtr xattr)
{
  VALUE result;

  // This node is already wrapped
  if (xattr->_private != NULL)
    return (VALUE) xattr->_private;

  result = Data_Wrap_Struct(cXMLAttrDecl, rxml_attr_decl_mark, NULL, xattr);

  xattr->_private = (void*) result;

  return result;
}

/*
 * call-seq:
 *    attr_decl.doc -> XML::Document
 *
 * Returns this attribute declaration's document.
 */
static VALUE rxml_attr_decl_doc_get(VALUE self)
{
  xmlAttributePtr xattr;
  Data_Get_Struct(self, xmlAttribute, xattr);
  if (xattr->doc == NULL)
    return Qnil;
  else
    return rxml_document_wrap(xattr->doc);
}


/*
 * call-seq:
 *    attr_decl.name -> "name"
 *
 * Obtain this attribute declaration's name.
 */
static VALUE rxml_attr_decl_name_get(VALUE self)
{
  xmlAttributePtr xattr;
  Data_Get_Struct(self, xmlAttribute, xattr);

  if (xattr->name == NULL)
    return Qnil;
  else
    return rxml_str_new2((const char*) xattr->name, xattr->doc->encoding);
}

/*
 * call-seq:
 *    attr_decl.next -> XML::AttrDecl
 *
 * Obtain the next attribute declaration.
 */
static VALUE rxml_attr_decl_next_get(VALUE self)
{
  xmlAttributePtr xattr;
  Data_Get_Struct(self, xmlAttribute, xattr);
  if (xattr->next == NULL)
    return Qnil;
  else
    return rxml_attr_decl_wrap((xmlAttributePtr)xattr->next);
}

/*
 * call-seq:
 *    attr_decl.type -> num
 *
 * Obtain this attribute declaration's type node type.
 */
static VALUE rxml_attr_decl_node_type(VALUE self)
{
  xmlAttrPtr xattr;
  Data_Get_Struct(self, xmlAttr, xattr);
  return INT2NUM(xattr->type);
}

/*
 * call-seq:
 *    attr_decl.parent -> XML::Dtd
 *
 * Obtain this attribute declaration's parent which
 * is an instance of a XML::DTD.
 */
static VALUE rxml_attr_decl_parent_get(VALUE self)
{
  xmlAttributePtr xattr;
  Data_Get_Struct(self, xmlAttribute, xattr);

  if (xattr->parent == NULL)
    return Qnil;
  else
    return rxml_dtd_wrap(xattr->parent);
}

/*
 * call-seq:
 *    attr_decl.prev -> (XML::AttrDecl | XML::ElementDecl)
 *
 * Obtain the previous attribute declaration or the owning
 * element declration (not implemented).
 */
static VALUE rxml_attr_decl_prev_get(VALUE self)
{
  xmlAttributePtr xattr;
  Data_Get_Struct(self, xmlAttribute, xattr);

  if (xattr->prev == NULL)
    return Qnil;
  else
    return rxml_attr_decl_wrap((xmlAttributePtr)xattr->prev);
}

/*
 * call-seq:
 *    attr_decl.value -> "value"
 *
 * Obtain the default value of this attribute declaration.
 */
VALUE rxml_attr_decl_value_get(VALUE self)
{
  xmlAttributePtr xattr;

  Data_Get_Struct(self, xmlAttribute, xattr);

  if (xattr->defaultValue)
    return rxml_str_new2((const char *)xattr->defaultValue, xattr->doc->encoding);
  else
    return Qnil;
}

void rxml_init_attr_decl(void)
{
  cXMLAttrDecl = rb_define_class_under(mXML, "AttrDecl", rb_cObject);
  rb_define_method(cXMLAttrDecl, "doc", rxml_attr_decl_doc_get, 0);
  rb_define_method(cXMLAttrDecl, "name", rxml_attr_decl_name_get, 0);
  rb_define_method(cXMLAttrDecl, "next", rxml_attr_decl_next_get, 0);
  rb_define_method(cXMLAttrDecl, "node_type", rxml_attr_decl_node_type, 0);
  rb_define_method(cXMLAttrDecl, "parent", rxml_attr_decl_parent_get, 0);
  rb_define_method(cXMLAttrDecl, "prev", rxml_attr_decl_prev_get, 0);
  rb_define_method(cXMLAttrDecl, "value", rxml_attr_decl_value_get, 0);
}
