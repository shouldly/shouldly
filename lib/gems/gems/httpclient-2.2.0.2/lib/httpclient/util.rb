# HTTPClient - HTTP client library.
# Copyright (C) 2000-2009  NAKAMURA, Hiroshi  <nahi@ruby-lang.org>.
#
# This program is copyrighted free software by NAKAMURA, Hiroshi.  You can
# redistribute it and/or modify it under the same terms of Ruby's license;
# either the dual license version in 2003, or any later version.


require 'uri'


unless ''.respond_to?(:bytesize)
  class String
    alias bytesize size
  end
end


class HTTPClient


  # A module for common function.
  module Util
    # Keyword argument helper.
    # args:: given arguments.
    # *field:: a list of arguments to be extracted.
    #
    # You can extract 3 arguments (a, b, c) with:
    #
    #   include Util
    #   def my_method(*args)
    #     a, b, c = keyword_argument(args, :a, :b, :c)
    #     ...
    #   end
    #   my_method(1, 2, 3)
    #   my_method(:b => 2, :a = 1)
    #
    # instead of;
    #
    #   def my_method(a, b, c)
    #     ...
    #   end
    #
    def keyword_argument(args, *field)
      if args.size == 1 and Hash === args[0]
        h = args[0]
        if field.any? { |f| h.key?(f) }
          return h.values_at(*field)
        end
      end
      args
    end

    # Keyword argument to hash helper.
    # args:: given arguments.
    # *field:: a list of arguments to be extracted.
    #
    # Returns hash which has defined keys. When a Hash given, returns it
    # including undefined keys. When an Array given, returns a Hash which only
    # includes defined keys.
    def argument_to_hash(args, *field)
      return nil if args.empty?
      if args.size == 1 and Hash === args[0]
        h = args[0]
        if field.any? { |f| h.key?(f) }
          return h
        end
      end
      h = {}
      field.each_with_index do |e, idx|
        h[e] = args[idx]
      end
      h
    end

    # Gets an URI instance.
    def urify(uri)
      if uri.nil?
        nil
      elsif uri.is_a?(URI)
        uri
      else
        URI.parse(uri.to_s)
      end
    end

    # Returns true if the given 2 URIs have a part_of relationship.
    # * the same scheme
    # * the same host String (no host resolution or IP-addr conversion)
    # * the same port number
    # * target URI's path starts with base URI's path.
    def uri_part_of(uri, part)
      ((uri.scheme == part.scheme) and
       (uri.host == part.host) and
       (uri.port == part.port) and
       uri.path.upcase.index(part.path.upcase) == 0)
    end
    module_function :uri_part_of

    # Returns parent directory URI of the given URI.
    def uri_dirname(uri)
      uri = uri.clone
      uri.path = uri.path.sub(/\/[^\/]*\z/, '/')
      uri
    end
    module_function :uri_dirname

    # Finds a value of a Hash.
    def hash_find_value(hash, &block)
      v = hash.find(&block)
      v ? v[1] : nil
    end
    module_function :hash_find_value

    # Checks if the given URI is https.
    def https?(uri)
      uri.scheme.downcase == 'https'
    end

    def force_binary(str)
      str.force_encoding(Encoding::ASCII_8BIT) if defined?(Encoding::ASCII_8BIT)
      str
    end
  end


end
