const quantityInput = document.getElementById("quantityInput");
const itemsTable = document.getElementById("itemsTable");
const totalInput = document.getElementById("totalInput");
const addButton = document.getElementById("addButton");
const productsSelect = document.getElementById("productsSelect");
let productList = [];
let cartItems = [];

addButton.addEventListener("click", () => {
    const productoId = productsSelect.value;
    const product = productList.find((p) => p.productoId == productoId);
    const quantity = parseInt(quantityInput.value);

    if (product && quantity > 0) {
        addItemToCart(product, quantity);
    } else {
        alert("Seleccione un producto y especifique una cantidad válida.");
    }
});

function removeProduct(productId) {
    cartItems = cartItems.filter((cartItem) => cartItem.product.productoId !== productId);
    updateCartTable();
}

async function fetchProductList() {
    const response = await fetch("http://localhost:5261/api/Producto/list");
    productList = await response.json();
    productList.forEach((product) => {
        const option = document.createElement("option");
        option.value = product.productoId;
        option.textContent = product.nombre;
        productsSelect.appendChild(option);
    });
}

function updateCartTable() {
    itemsTable.innerHTML = `
    <tbody>
      ${cartItems
            .map(
                (cartItem) =>
                    `<tr>
              <td>${cartItem.product.nombre}</td>
              <td>${cartItem.product.precio}</td>
              <td>${cartItem.quantity}</td>
              <td>${cartItem.product.precio * cartItem.quantity}</td>
              <td>
                <button class="custom-button" onClick="removeProduct(${cartItem.product.productoId
                    })">
                  Eliminar
                </button>
              </td>
            </tr>`
            )
            .join("")}
    </tbody>
  `;

    totalInput.value = cartItems.reduce(
        (total, cartItem) => total + cartItem.product.precio * cartItem.quantity,
        0
    );
}

function addItemToCart(product, quantity) {
    const cartItem = {
        product: product,
        quantity: quantity,
    };
    const existingCartItem = cartItems.find((i) => i.product.productoId === product.productoId);
    if (existingCartItem) {
        existingCartItem.quantity += quantity;
    } else {
        cartItems.push(cartItem);
    }
    updateCartTable();
}

fetchProductList();

