$(function () {
    $("#AddProductForm").validate({
        rules: {
            CustomerID: { required: true },
            ProductCode: { required: true },
            Qty: {
                required: true,
                regex: '^[0-9]{0,3}$'
            }
        },
        messages: {
            Qty: {
                regex: 'must be a number. maximum 100'
            }
        }
    });

    $("#editOrderProductForm").validate({
        rules: {
            CustomerID: { required: true },
            ProductCode: { required: true },
            Qty: {
                required: true
            }
        },
        messages: {
        }
    });

});