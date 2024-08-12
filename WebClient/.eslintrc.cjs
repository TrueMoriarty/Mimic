module.exports = {
    root: true,
    env: { browser: true, es2020: true },
    extends: [
        'eslint:recommended',
        'plugin:react/recommended',
        'plugin:react/jsx-runtime',
        'plugin:react-hooks/recommended',
    ],
    ignorePatterns: ['dist', '.eslintrc.cjs'],
    parserOptions: {
        ecmaFeatures: {
            jsx: true,
        },
        ecmaVersion: 'latest',
        sourceType: 'module',
    },
    settings: { react: { version: '18.2' } },
    plugins: ['react-refresh', 'react', 'prettier'],
    rules: {
        'prettier/prettier': 'error', // Обязательно!! Подсвечивает ошибки из Prettier.
        'react/prop-types': 0, // Отключаем правило проверки передаваемых типов.
    },
};
