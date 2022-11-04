namespace downtown.Constants
{
    public class SqlQuery
    {
        //get Department List
        public const string GetDepartment = "select * from department";
        //save register details in register table
        public const string SaveRegisterDetails = "insert into register  values(@FirstName,@LastName,@Address,@Email,@PhoneNumber,@Password)";
        //Get Email Address and Password
        public const string GetEmailAddressAndPassword = "select Email,Password from register where Email=@Email";
        //get email address
        public const string GetEmailAddress= "select Email from register where Email=@Email";
        //get register table details
        public const string GetRegisterDetails= "select * from register";
        //save product details
        public const string SaveProductDetails = "insert into product_table values(@ProductName,@ProductDesc,@ProductPrice,@ProductQuantity,@ProductImage)";
        //get product details
        //public const string GetProductDetails = "select * from product_table";

        public const string GetProductDetails = "SELECT productname,productdesc,productprice,productquantity,CONVERT(VARCHAR(max), productimage,0) as productimage from product_table";
    }
}
