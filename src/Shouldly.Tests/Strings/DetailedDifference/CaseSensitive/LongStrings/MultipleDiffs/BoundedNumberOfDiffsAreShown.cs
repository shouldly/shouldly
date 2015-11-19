using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive.LongStrings.MultipleDiffs
{
    public class BoundedNumberOfDiffsAreShown : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            @"1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2a,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3a,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4a,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5a,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6a,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7a,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8a,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9a,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z".Replace("\r\n", "\n")
             .ShouldBe(
            @"1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2a,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3a,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4a,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5a,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6a,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7a,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8a,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9a,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z".Replace("\r\n", "\n"));
        }

        protected override void ShouldThrowAWobbly()
        {
            @"1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2a,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3a,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4a,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5a,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6a,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7a,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8a,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9a,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z".Replace("\r\n", "\n")
             .ShouldBe(
            @"1A,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2A,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3A,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4A,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5A,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6A,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7A,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8A,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9A,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z".Replace("\r\n", "\n"));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get 
            {
                return @"@""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2a,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3a,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4a,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5a,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6a,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7a,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8a,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9a,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z"".Replace(""\r\n"", ""\n"")
        should be
    ""1A,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2A,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3A,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4A,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5A,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6A,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7A,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8A,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9A,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z""
        but was
    ""1a,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1s,1t,1u,1v,1w,1x,1y,1z,
              2a,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2s,2t,2u,2v,2w,2x,2y,2z,
              3a,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3s,3t,3u,3v,3w,3x,3y,3z,
              4a,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4s,4t,4u,4v,4w,4x,4y,4z,
              5a,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5s,5t,5u,5v,5w,5x,5y,5z,
              6a,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6s,6t,6u,6v,6w,6x,6y,6z,
              7a,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7s,7t,7u,7v,7w,7x,7y,7z,
              8a,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8s,8t,8u,8v,8w,8x,8y,8z,
              9a,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z""
        difference
    Case and Line Ending Sensitive Comparison
Difference     |       |                                                                                                       
               |      \|/                                                                                                      
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
Expected Value | 1    A    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Expected Code  | 49   65   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  
Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  84   85   86   87   88   89   90   91   92   93   94   95   96   97   98   99   100  101  102  103  104  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   2    A    ,    2    b    ,    2    c    ,    2    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   2    a    ,    2    b    ,    2    c    ,    2    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   50   65   44   50   98   44   50   99   44   50   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   50   97   44   50   98   44   50   99   44   50   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  177  178  179  180  181  182  183  184  185  186  187  188  189  190  191  192  193  194  195  196  197  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   3    A    ,    3    b    ,    3    c    ,    3    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   3    a    ,    3    b    ,    3    c    ,    3    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   51   65   44   51   98   44   51   99   44   51   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   51   97   44   51   98   44   51   99   44   51   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  270  271  272  273  274  275  276  277  278  279  280  281  282  283  284  285  286  287  288  289  290  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   4    A    ,    4    b    ,    4    c    ,    4    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   4    a    ,    4    b    ,    4    c    ,    4    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   52   65   44   52   98   44   52   99   44   52   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   52   97   44   52   98   44   52   99   44   52   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  363  364  365  366  367  368  369  370  371  372  373  374  375  376  377  378  379  380  381  382  383  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   5    A    ,    5    b    ,    5    c    ,    5    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   5    a    ,    5    b    ,    5    c    ,    5    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   53   65   44   53   98   44   53   99   44   53   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   53   97   44   53   98   44   53   99   44   53   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  456  457  458  459  460  461  462  463  464  465  466  467  468  469  470  471  472  473  474  475  476  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   6    A    ,    6    b    ,    6    c    ,    6    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   6    a    ,    6    b    ,    6    c    ,    6    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   54   65   44   54   98   44   54   99   44   54   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   54   97   44   54   98   44   54   99   44   54   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  549  550  551  552  553  554  555  556  557  558  559  560  561  562  563  564  565  566  567  568  569  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   7    A    ,    7    b    ,    7    c    ,    7    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   7    a    ,    7    b    ,    7    c    ,    7    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   55   65   44   55   98   44   55   99   44   55   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   55   97   44   55   98   44   55   99   44   55   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  642  643  644  645  646  647  648  649  650  651  652  653  654  655  656  657  658  659  660  661  662  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   8    A    ,    8    b    ,    8    c    ,    8    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   8    a    ,    8    b    ,    8    c    ,    8    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   56   65   44   56   98   44   56   99   44   56   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   56   97   44   56   98   44   56   99   44   56   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  735  736  737  738  739  740  741  742  743  744  745  746  747  748  749  750  751  752  753  754  755  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   9    A    ,    9    b    ,    9    c    ,    9    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   9    a    ,    9    b    ,    9    c    ,    9    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   57   65   44   57   98   44   57   99   44   57   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   57   97   44   57   98   44   57   99   44   57   100  44   ... "
                    ;
                }
        }
    }
}
