using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive.LongStrings.MultipleDiffs
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
              9a,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9s,9t,9u,9v,9w,9x,9y,9z".Replace("\r\n", "\n"),
             ShouldBeStringOptions.IgnoreCase);
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
            @"1b,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1w,1t,1u,1v,1w,1x,1y,1z,
              2b,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2w,2t,2u,2v,2w,2x,2y,2z,
              3b,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3w,3t,3u,3v,3w,3x,3y,3z,
              4b,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4w,4t,4u,4v,4w,4x,4y,4z,
              5b,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5w,5t,5u,5v,5w,5x,5y,5z,
              6b,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6w,6t,6u,6v,6w,6x,6y,6z,
              7b,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7w,7t,7u,7v,7w,7x,7y,7z,
              8b,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8w,8t,8u,8v,8w,8x,8y,8z,
              9b,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9w,9t,9u,9v,9w,9x,9y,9z".Replace("\r\n", "\n"),
             ShouldBeStringOptions.IgnoreCase);
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
    ""1b,1b,1c,1d,1e,1f,1g,1h,1i,1j,1k,1l,1m,1n,1o,1p,1q,1r,1w,1t,1u,1v,1w,1x,1y,1z,
              2b,2b,2c,2d,2e,2f,2g,2h,2i,2j,2k,2l,2m,2n,2o,2p,2q,2r,2w,2t,2u,2v,2w,2x,2y,2z,
              3b,3b,3c,3d,3e,3f,3g,3h,3i,3j,3k,3l,3m,3n,3o,3p,3q,3r,3w,3t,3u,3v,3w,3x,3y,3z,
              4b,4b,4c,4d,4e,4f,4g,4h,4i,4j,4k,4l,4m,4n,4o,4p,4q,4r,4w,4t,4u,4v,4w,4x,4y,4z,
              5b,5b,5c,5d,5e,5f,5g,5h,5i,5j,5k,5l,5m,5n,5o,5p,5q,5r,5w,5t,5u,5v,5w,5x,5y,5z,
              6b,6b,6c,6d,6e,6f,6g,6h,6i,6j,6k,6l,6m,6n,6o,6p,6q,6r,6w,6t,6u,6v,6w,6x,6y,6z,
              7b,7b,7c,7d,7e,7f,7g,7h,7i,7j,7k,7l,7m,7n,7o,7p,7q,7r,7w,7t,7u,7v,7w,7x,7y,7z,
              8b,8b,8c,8d,8e,8f,8g,8h,8i,8j,8k,8l,8m,8n,8o,8p,8q,8r,8w,8t,8u,8v,8w,8x,8y,8z,
              9b,9b,9c,9d,9e,9f,9g,9h,9i,9j,9k,9l,9m,9n,9o,9p,9q,9r,9w,9t,9u,9v,9w,9x,9y,9z""
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
    Case Insensitive and Line Ending Sensitive Comparison
Showing some of the 18 differences
Difference     |       |                                                                                                       
               |      \|/                                                                                                      
Index          | 0    1    2    3    4    5    6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   ...  
Expected Value | 1    b    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Actual Value   | 1    a    ,    1    b    ,    1    c    ,    1    d    ,    1    e    ,    1    f    ,    1    g    ,    ...  
Expected Code  | 49   98   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  
Actual Code    | 49   97   44   49   98   44   49   99   44   49   100  44   49   101  44   49   102  44   49   103  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  45   46   47   48   49   50   51   52   53   54   55   56   57   58   59   60   61   62   63   64   65   ...  
Expected Value | ...  1    p    ,    1    q    ,    1    r    ,    1    w    ,    1    t    ,    1    u    ,    1    v    ,    ...  
Actual Value   | ...  1    p    ,    1    q    ,    1    r    ,    1    s    ,    1    t    ,    1    u    ,    1    v    ,    ...  
Expected Code  | ...  49   112  44   49   113  44   49   114  44   49   119  44   49   116  44   49   117  44   49   118  44   ...  
Actual Code    | ...  49   112  44   49   113  44   49   114  44   49   115  44   49   116  44   49   117  44   49   118  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  84   85   86   87   88   89   90   91   92   93   94   95   96   97   98   99   100  101  102  103  104  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   2    b    ,    2    b    ,    2    c    ,    2    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   2    a    ,    2    b    ,    2    c    ,    2    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   50   98   44   50   98   44   50   99   44   50   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   50   97   44   50   98   44   50   99   44   50   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  138  139  140  141  142  143  144  145  146  147  148  149  150  151  152  153  154  155  156  157  158  ...  
Expected Value | ...  2    p    ,    2    q    ,    2    r    ,    2    w    ,    2    t    ,    2    u    ,    2    v    ,    ...  
Actual Value   | ...  2    p    ,    2    q    ,    2    r    ,    2    s    ,    2    t    ,    2    u    ,    2    v    ,    ...  
Expected Code  | ...  50   112  44   50   113  44   50   114  44   50   119  44   50   116  44   50   117  44   50   118  44   ...  
Actual Code    | ...  50   112  44   50   113  44   50   114  44   50   115  44   50   116  44   50   117  44   50   118  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  177  178  179  180  181  182  183  184  185  186  187  188  189  190  191  192  193  194  195  196  197  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   3    b    ,    3    b    ,    3    c    ,    3    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   3    a    ,    3    b    ,    3    c    ,    3    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   51   98   44   51   98   44   51   99   44   51   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   51   97   44   51   98   44   51   99   44   51   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  231  232  233  234  235  236  237  238  239  240  241  242  243  244  245  246  247  248  249  250  251  ...  
Expected Value | ...  3    p    ,    3    q    ,    3    r    ,    3    w    ,    3    t    ,    3    u    ,    3    v    ,    ...  
Actual Value   | ...  3    p    ,    3    q    ,    3    r    ,    3    s    ,    3    t    ,    3    u    ,    3    v    ,    ...  
Expected Code  | ...  51   112  44   51   113  44   51   114  44   51   119  44   51   116  44   51   117  44   51   118  44   ...  
Actual Code    | ...  51   112  44   51   113  44   51   114  44   51   115  44   51   116  44   51   117  44   51   118  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  270  271  272  273  274  275  276  277  278  279  280  281  282  283  284  285  286  287  288  289  290  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   4    b    ,    4    b    ,    4    c    ,    4    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   4    a    ,    4    b    ,    4    c    ,    4    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   52   98   44   52   98   44   52   99   44   52   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   52   97   44   52   98   44   52   99   44   52   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  324  325  326  327  328  329  330  331  332  333  334  335  336  337  338  339  340  341  342  343  344  ...  
Expected Value | ...  4    p    ,    4    q    ,    4    r    ,    4    w    ,    4    t    ,    4    u    ,    4    v    ,    ...  
Actual Value   | ...  4    p    ,    4    q    ,    4    r    ,    4    s    ,    4    t    ,    4    u    ,    4    v    ,    ...  
Expected Code  | ...  52   112  44   52   113  44   52   114  44   52   119  44   52   116  44   52   117  44   52   118  44   ...  
Actual Code    | ...  52   112  44   52   113  44   52   114  44   52   115  44   52   116  44   52   117  44   52   118  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  363  364  365  366  367  368  369  370  371  372  373  374  375  376  377  378  379  380  381  382  383  ...  
Expected Value | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   5    b    ,    5    b    ,    5    c    ,    5    d    ,    ...  
Actual Value   | ...  \s   \s   \s   \s   \s   \s   \s   \s   \s   5    a    ,    5    b    ,    5    c    ,    5    d    ,    ...  
Expected Code  | ...  32   32   32   32   32   32   32   32   32   53   98   44   53   98   44   53   99   44   53   100  44   ...  
Actual Code    | ...  32   32   32   32   32   32   32   32   32   53   97   44   53   98   44   53   99   44   53   100  44   ...  

Difference     |                                                         |                                                          
               |                                                        \|/                                                         
Index          | ...  417  418  419  420  421  422  423  424  425  426  427  428  429  430  431  432  433  434  435  436  437  ...  
Expected Value | ...  5    p    ,    5    q    ,    5    r    ,    5    w    ,    5    t    ,    5    u    ,    5    v    ,    ...  
Actual Value   | ...  5    p    ,    5    q    ,    5    r    ,    5    s    ,    5    t    ,    5    u    ,    5    v    ,    ...  
Expected Code  | ...  53   112  44   53   113  44   53   114  44   53   119  44   53   116  44   53   117  44   53   118  44   ...  
Actual Code    | ...  53   112  44   53   113  44   53   114  44   53   115  44   53   116  44   53   117  44   53   118  44   ...  "
                    ;
                }
        }
    }
}
