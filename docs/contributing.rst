Contributing
=================

Once you have cloned Shouldly to your local machine, the following instructions will walk you through installing the tools necessary to build and test the documentation.

1. `Download python <https://www.python.org/downloads/>`_ version 2.7.10 or higher.

2. If you are installing on Windows, add both the Python install directory and the Python scripts directory to your ``PATH`` environment variable. For example, if you install Python into the ``c:\python34`` directory, you would add ``c:\python34;c:\python34\scripts`` to your ``PATH`` environment variable.

3. Install Sphinx by opening a command prompt and running the following Python command. (Note that this operation might take a few minutes to complete.)::

	pip install sphinx

4. By default, when you install Sphinx, it will install the ReadTheDocs custom theme automatically. If you need to update the installed version of this theme, you should run::

	pip install -U sphinx_rtd_theme

5. Run the `make.bat` file using `html` argument to build the stand-alone version of the project in question::

	make html

6. Once make completes, the generated docs will be in the ``_build/html`` directory. Simply open the ``index.html`` file in your browser to see the built docs for that project.

Style Guidelines
----------------

Please review the following style guides:

- `Sphinx Style Guide <http://documentation-style-guide-sphinx.readthedocs.org/en/latest/style-guide.html>`_
- `ASP.NET Docs Style Guide <http://docs.asp.net/en/latest/contribute/style-guide.html>`_
