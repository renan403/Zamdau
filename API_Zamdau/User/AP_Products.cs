namespace API_Zamdau.User
{
    public class AP_OrderItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class AP_ListProduct
    {
        public string Id { get; set; }
        public AP_Product Product { get; set; }
    }

    public class AP_Product
    {
        public string Id { get; set; } // Identificador único do produto
        public string Uid { get; set; } // Identificador único do usuário
        public int Quantity { get; set; } // Quantidade em estoque
        public double Price { get; set; } // Preço do produto
        public string Name { get; set; } // Nome do produto
        public string Description { get; set; } // Descrição do produto
        public string Brand { get; set; } // Marca do produto
        public string? Code { get; set; } // Código opcional do produto
        public string ImageUrl { get; set; } // URL da imagem
        public int? Rating { get; set; }
        public int? ReviewCount { get; set; }
        public List<AP_Specification> Specifications { get; set; } = []; // Especificações do produto
        public Dictionary<string, AP_Comment> Comments { get; set; } = new();
        // Comentários do produto
    }

    public class AP_Comment
    {
        public string? OrderNumber { get; set; } // Número do pedido (opcional)
        public string CustomerId { get; set; } // ID do cliente
        public string ProductId { get; set; } // ID do produto
        public string Author { get; set; } // Nome do autor do comentário
        public string Text { get; set; } // Texto do comentário
        public DateTime DatePosted { get; set; } = DateTime.UtcNow; // Data do comentário
        public int Rating { get; set; } // Avaliação (de 1 a 5)
    }

    public class AP_Specification
    {
        public string Name { get; set; } // Nome da especificação
        public string Value { get; set; } // Valor da especificação
    }

}
