/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: 'media',
    content: [
        "./**/*.{razor,html,cshtml}",
        "./node_modules/flowbite/**/*.{js,razor,html,cshtml}",
        "./wwwroot/**/*.js"
    ],
    theme: {
        extend: {
            fontSize: {
                'xxs': '10px',  // Define a custom font size smaller than 'text-xs'
            },
            width: {
                '52': '13rem'
            }
        },
    },
    plugins: [
        require('flowbite/plugin')
    ],
}

