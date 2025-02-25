import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
    server: {
        port: 5174,
        https: {
            key: fs.readFileSync(
                path.resolve(__dirname, 'dev_certificates/localhost-key.pem')
            ),
            cert: fs.readFileSync(
                path.resolve(__dirname, 'dev_certificates/localhost.pem')
            ),
        },
    },
    plugins: [react()],
});
