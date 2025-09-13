class AccessibilityOptions {
    constructor() {
        this.init();
    }

    init() {
        console.log('Accessibility options initialized');
        this.loadSavedOptions();
        this.setupEventListeners();
    }

    loadSavedOptions() {
        const fontSize = localStorage.getItem('fontSize') || 'normal';
        const contrast = localStorage.getItem('contrast') || 'normal';

        console.log('Loading saved options:', { fontSize, contrast });

        this.applyFontSize(fontSize);
        this.applyContrast(contrast);
    }

    setupEventListeners() {
        console.log('Setting up event listeners');

        // Usar event delegation para mayor confiabilidad
        document.addEventListener('click', (e) => {
            if (e.target.matches('.font-size-btn, .contrast-btn, #reset-accessibility')) {
                e.preventDefault();

                if (e.target.id === 'font-small') this.setFontSize('small');
                else if (e.target.id === 'font-normal') this.setFontSize('normal');
                else if (e.target.id === 'font-large') this.setFontSize('large');
                else if (e.target.id === 'font-xlarge') this.setFontSize('xlarge');
                else if (e.target.id === 'contrast-normal') this.setContrast('normal');
                else if (e.target.id === 'contrast-high') this.setContrast('high');
                else if (e.target.id === 'contrast-inverted') this.setContrast('inverted');
                else if (e.target.id === 'reset-accessibility') this.resetOptions();
            }
        });
    }

    setFontSize(size) {
        console.log('Setting font size to:', size);
        localStorage.setItem('fontSize', size);
        this.applyFontSize(size);
    }

    setContrast(contrast) {
        console.log('Setting contrast to:', contrast);
        localStorage.setItem('contrast', contrast);
        this.applyContrast(contrast);
    }

    applyFontSize(size) {
        const sizes = {
            'small': '14px',
            'normal': '16px',
            'large': '18px',
            'xlarge': '20px'
        };

        // Aplicar directamente al body con !important mediante style attribute
        document.body.style.setProperty('font-size', sizes[size], 'important');

        // También establecer variable CSS
        document.documentElement.style.setProperty('--current-font-size', sizes[size]);

        this.updateActiveButtons('font', size);
    }

    applyContrast(contrast) {
        console.log('Applying contrast:', contrast);

        // Remover todas las clases de contraste
        document.body.classList.remove('high-contrast', 'inverted-contrast');

        // Aplicar nueva clase
        if (contrast === 'high') {
            document.body.classList.add('high-contrast');
        } else if (contrast === 'inverted') {
            document.body.classList.add('inverted-contrast');
        }

        this.updateActiveButtons('contrast', contrast);
    }

    updateActiveButtons(type, value) {
        // Remover active de todos los botones del tipo
        document.querySelectorAll(`.${type}-size-btn, .${type}-btn`).forEach(btn => {
            btn.classList.remove('active');
        });

        // Agregar active al botón correspondiente
        const activeBtn = document.getElementById(`${type}-${value}`);
        if (activeBtn) {
            activeBtn.classList.add('active');
        }
    }

    resetOptions() {
        console.log('Resetting accessibility options');
        localStorage.removeItem('fontSize');
        localStorage.removeItem('contrast');

        // Resetear estilos
        document.body.style.fontSize = '';
        document.body.classList.remove('high-contrast', 'inverted-contrast');

        this.updateActiveButtons('font', 'normal');
        this.updateActiveButtons('contrast', 'normal');
    }
}

// Inicialización mejorada
function initAccessibility() {
    // Esperar a que el DOM esté completamente cargado
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', () => {
            window.accessibility = new AccessibilityOptions();
        });
    } else {
        window.accessibility = new AccessibilityOptions();
    }
}

// Función global para el toggle
window.toggleAccessibilityPanel = function () {
    const panel = document.getElementById('accessibilityOptions');
    if (panel) {
        panel.classList.toggle('show');
        console.log('Panel visibility toggled');
    }
}

/*document.addEventListener('click', function (e) {
    const panel = document.getElementById('accessibilityOptions');
    const btn = document.querySelector('.accessibility-btn');

    if (!panel.contains(e.target) && e.target !== btn) {
        panel.classList.remove('show');
    }
});*/

function toggleAccessibilityPanel() {
    const panel = document.getElementById('accessibilityOptions');
    panel.classList.toggle('show');
}

// Iniciar
initAccessibility();