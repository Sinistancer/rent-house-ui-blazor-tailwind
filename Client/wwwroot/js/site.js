window.hideLoadingMessage = function () {
    var loadingMessage = document.getElementById("loadingMessage");
    if (loadingMessage !== null) {
        loadingMessage.classList.add("hidden");
    }

    var textMessage = document.getElementById("textMessage");
    if (textMessage !== null) {
        textMessage.classList.remove("hidden");
    }
}

window.visibilityElement = function (tagId) {
    var element = document.getElementById(tagId);

    checkBodyClass(element);
}

window.handleResize = function () {
    var logoSidebar = document.getElementById("logo-sidebar");
    var body = document.getElementById("body");

    if (logoSidebar !== null) {
        if (window.matchMedia("(max-width: 768px)").matches) {
            logoSidebar.classList.remove('transform-none');
            logoSidebar.classList.add('-translate-x-full');

            var checkBody = body.classList.contains("overflow-hidden");
            if (checkBody) {
                body.classList.remove("overflow-hidden");
            }

        } else {
            logoSidebar.classList.remove('-translate-x-full');
            logoSidebar.classList.add('transform-none');

            var checkBody = body.classList.contains("overflow-hidden");
            if (checkBody) {
                body.classList.remove("overflow-hidden");
            }
        }
    }
}

window.checkBodyClass = function (element) {
    var body = document.getElementById("body");
    var drawerBackdrop = document.getElementById("drawer-backdrop");

    if (body !== null) {
        var checkBody = body.classList.contains("overflow-hidden");

        if (!checkBody) {
            body.classList.add("overflow-hidden");

            if (drawerBackdrop == null) {
                var newDrawerBackdrop = document.createElement('div');

                newDrawerBackdrop.setAttribute('id', 'drawer-backdrop');
                newDrawerBackdrop.setAttribute('drawer-backdrop', '');
                newDrawerBackdrop.setAttribute('onclick', 'handleClick("' + element.id + '")');
                newDrawerBackdrop.classList.add('bg-gray-900/50', 'dark:bg-gray-900/80', 'fixed', 'inset-0');

                if (element.id.includes("modal")) {
                    newDrawerBackdrop.classList.add('z-40');
                } else {
                    newDrawerBackdrop.classList.add('z-30');
                }

                body.appendChild(newDrawerBackdrop);
            }

            if (element !== null) {
                if (element.id == "logo-sidebar") {
                    element.classList.remove('-translate-x-full');
                    element.classList.add('transform-none');
                }

                if (element.id.includes("modal")) {
                    element.classList.remove('hidden');
                    element.classList.add('flex');
                }

                element.removeAttribute('aria-hidden');
                element.setAttribute('aria-modal', true);
                element.setAttribute('role', 'dialog');
            }
        } else {
            body.classList.remove("overflow-hidden");

            if (drawerBackdrop != null) {
                drawerBackdrop.remove()
            }

            if (element !== null) {
                if (element.id == "logo-sidebar") {
                    element.classList.remove('transform-none');
                    element.classList.add('-translate-x-full');
                }

                if (element.id.includes("modal")) {
                    element.classList.remove('flex');
                    element.classList.add('hidden');
                }

                element.setAttribute('aria-hidden', true);
                element.removeAttribute('aria-modal');
                element.removeAttribute('role');
            }
        }
    }
}

window.toggleModal = function (attributeId) {
    var getElement = document.getElementById(attributeId);

    checkBodyClass(getElement);
}

window.handleClick = function (tagId) {
    visibilityElement(tagId);
}

// Run the function once to set the initial state
handleResize();

// Add event listener for window resize
window.addEventListener('resize', handleResize);

window.validateInput = (inputElement, minValue, maxValue) => {
    if (inputElement instanceof HTMLInputElement) {
        const form = inputElement.closest('form');
        if (form) {
            form.addEventListener('submit', (event) => {
                const value = parseCurrency(inputElement.value);

                if (isNaN(value) || value < minValue || value > maxValue) {
                    inputElement.setCustomValidity(`The value must be between ${formatCurrency(minValue)} and ${formatCurrency(maxValue)}.`);
                    inputElement.reportValidity();
                    event.preventDefault();
                } else {
                    inputElement.setCustomValidity('');
                }
            });

            inputElement.addEventListener('input', () => {
                const value = parseCurrency(inputElement.value);

                if (isNaN(value) || value < minValue || value > maxValue) {
                    inputElement.setCustomValidity(`The value must be between ${formatCurrency(minValue)} and ${formatCurrency(maxValue)}.`);
                    inputElement.reportValidity();
                } else {
                    inputElement.setCustomValidity('');
                }
            });
        }
    }
};


window.confirmPassword = (inputElement, password, confirmPassword) => {
    if (inputElement instanceof HTMLInputElement) {
        const form = inputElement.closest('form');
        if (form) {
            if (password != confirmPassword) {
                inputElement.setCustomValidity(`The value for password and confirm password must be same.`);
                inputElement.reportValidity();
            } else {
                inputElement.setCustomValidity('');
            }
        }
    }
};

window.validateEmail = (inputElement) => {
    if (inputElement instanceof HTMLInputElement) {
        const form = inputElement.closest('form');
        if (form) {
            form.addEventListener('submit', (event) => {
                const value = inputElement.value;
                if (!isValidEmail(value)) {
                    inputElement.setCustomValidity('The email format is not valid.');
                    inputElement.reportValidity();
                    event.preventDefault();
                } else {
                    inputElement.setCustomValidity('');
                }
            });

            inputElement.addEventListener('input', () => {
                const value = inputElement.value;
                if (!isValidEmail(value)) {
                    inputElement.setCustomValidity('The email format is not valid.');
                    inputElement.reportValidity();
                } else {
                    inputElement.setCustomValidity('');
                }
            });
        }
    }
};

function isValidEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

function parseCurrency(value) {
    // Remove currency formatting and convert to number
    return parseFloat(value.replace(/[^0-9,-]+/g, '').replace('.', '').replace(',', '.'));
}

function formatCurrency(value) {
    // Format number as currency
    return new Intl.NumberFormat('id-ID', {
        style: 'currency',
        currency: 'IDR',
        maximumFractionDigits: 0,
        minimumFractionDigits: 0
    }).format(value);
}